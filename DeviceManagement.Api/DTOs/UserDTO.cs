using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.Api.DTOs;

public class UserDTO
{
    [Required] public string Name { get; set; }
    [Required] public string Role { get; set; }
    [Required] public string Location { get; set; }
}