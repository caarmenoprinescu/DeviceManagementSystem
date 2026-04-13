using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Models;
using DeviceManagement.Api.Repositories.Interfaces;
using DeviceManagement.Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DeviceManagement.Api.Services;

public class AuthService(IUserRepository repository, IConfiguration configuration) : IAuthService
{
    private readonly IUserRepository _userRepository = repository;
    private readonly IConfiguration _configuration = configuration;

    private string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<AuthResponseDTO> Register(RegisterDTO register)
    {
        register.Email = register.Email.Trim().ToLower();
        var exists = await _userRepository.GetUserByEmailAsync(register.Email);

        if (exists != null)
            throw new Exception($"Email {register.Email} already registered. Go to login!");


        string hashPassword = BCrypt.Net.BCrypt.HashPassword(register.Password);
        var user = new User
        {
            Name = register.Name,
            Role = "User",
            Location = "Unknown",
            Email = register.Email,
            PasswordHash = hashPassword,
        };
        int newId = await _userRepository.AddAuthUserAsync(user);
        var newUser = await _userRepository.GetUserByIdAsync(newId);

        var token = GenerateToken(newUser);
        return new AuthResponseDTO
        {
            UserId = newUser.Id,
            Token = token,
            UserName = newUser.Name
        };
    }


    public async Task<AuthResponseDTO> Login(LoginDTO login)
    {
        var user = await _userRepository.GetUserByEmailAsync(login.Email);
        
      
        if (user == null)
            throw new Exception("Invalid credentials");
        bool isValid = BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash);
        if (isValid)
        {
            var token = GenerateToken(user);
            return new AuthResponseDTO
            {
                UserName = user.Name,
                UserId = user.Id,
                Token = token
            };
        }

        throw new Exception("Invalid credentials");
    }
}