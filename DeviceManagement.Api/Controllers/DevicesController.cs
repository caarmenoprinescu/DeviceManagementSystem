using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DevicesController(IDeviceService deviceService, IDescriptionService descriptionService) : ControllerBase
{
    private readonly IDeviceService _deviceService = deviceService;
    private readonly IDescriptionService _descriptionService = descriptionService;
    [HttpGet]
    public async Task<IActionResult> GetDevices()
    {
        var devices = await _deviceService.GetAllDevicesAsync();
        return Ok(devices);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDeviceById(int id)
    {
        var device = await _deviceService.GetDeviceByIdAsync(id);
        if (device == null) return NotFound($"Device with id {id} not found.");
        return Ok(device);
    }

    [HttpPost]
    public async Task<IActionResult> AddDevice([FromBody] DeviceDTO device)
    {
        try
        {
            var id = await _deviceService.AddDeviceAsync(device);
            var created = await _deviceService.GetDeviceByIdAsync(id);

            return CreatedAtAction(nameof(GetDeviceById), new { id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDevice(int id, DeviceDTO device)
    {
        try
        {
            await _deviceService.UpdateDeviceAsync(id, device);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDevice(int id)
    {
        try
        {
            await _deviceService.DeleteDeviceAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPost("generate-description")]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateDescription([FromBody] GenerateDescriptionDTO dto)
    {
        
        var prompt = $"Generate a short, professional and user friendly description for a device with these specs: " +
                     $"Name: {dto.Name}, Manufacturer: {dto.Manufacturer}, Type: {dto.Type}, " +
                     $"OS: {dto.OperatingSystem}, Processor: {dto.Processor}, RAM: {dto.Ram}GB. " +
                     $"Keep it under 2 sentences.  Take as example for the following device Input: Name – iPhone 17 Pro, Manufacturer – Apple, OS – iOS, Type – phone, RAM – 12GB, Processor – A19 Pro this output: “A high-performance Apple smartphone running iOS, suitable for daily business use.";

        var description = await _descriptionService.GenerateDescriptionAsync(prompt);
       
        return Ok(description);
    }
}