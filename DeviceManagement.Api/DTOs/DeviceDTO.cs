using System.ComponentModel.DataAnnotations;
using DeviceManagement.Api.Models;

namespace DeviceManagement.Api.DTOs;

public class DeviceDTO
{
    [Required] public string Name { get; set; } = string.Empty;

    [Required] public string Manufacturer { get; set; } = string.Empty;

    public DeviceType Type { get; set; }

    [Required] public string OperatingSystem { get; set; } = string.Empty;

    [Required] public string OsVersion { get; set; } = string.Empty;

    [Required] public string Processor { get; set; } = string.Empty;

    public int Ram { get; set; }

    [Required] public string Description { get; set; } = string.Empty;

    public int? UserId { get; set; }
}