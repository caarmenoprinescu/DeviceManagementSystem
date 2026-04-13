using DeviceManagement.Api.DTOs;

namespace DeviceManagement.Api.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDTO> Register(RegisterDTO register);
    Task<AuthResponseDTO> Login(LoginDTO login);
}