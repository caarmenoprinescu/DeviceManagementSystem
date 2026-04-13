using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Repositories.Interfaces;
using DeviceManagement.Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DeviceManagement.Api.Services;

public class AuthService(IUserService service, IUserRepository repository, IConfiguration configuration) : IAuthService
{
    private readonly IUserService _userService = service;
    private readonly IUserRepository _userRepository = repository;
    private readonly IConfiguration _configuration = configuration;
    private string GenerateToken(int userId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
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
        string hashPassword = BCrypt.Net.BCrypt.HashPassword(register.Password);
        //prepare data to add the new user:
        var user = new UserDTO
        {
            Name = register.Name,
            Role = "User",
            Location = "Unknown"
        };
        int userId = await _userRepository.AddUserAsync(user);
        GenerateToken(userId);
    }


    public async Task<AuthResponseDTO> Login(LoginDTO login)
    {
        string hashPassword = BCrypt.Net.BCrypt.HashPassword(login.Password);
        var user = await _userRepository.GetUserByEmailAsync(login.Email);
        bool isValid = BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash);
        if (isValid)
            return new AuthResponseDTO
            {
                UserName = user.Name,
                UserId = user.Id,
                Token = generatetoken(user.Id);
            }
        else throw new Exception("Incorrect password. Try again!");
    }
}