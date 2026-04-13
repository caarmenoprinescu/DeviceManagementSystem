using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.Api.DTOs;

public class RegisterDTO
{
   [Required] public string Name { get; set; }
   [Required] public string Email { get; set; }
   [Required] public string Password { get; set; }
}