namespace DeviceManagement.Api.DTOs;

public class AuthResponseDTO
{
    public string Token { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
}