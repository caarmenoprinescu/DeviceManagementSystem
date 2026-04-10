using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController(IDeviceService deviceService) : ControllerBase
{
    private readonly IDeviceService _deviceService = deviceService;

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
        var id = await _deviceService.AddDeviceAsync(device);
        var created = await _deviceService.GetDeviceByIdAsync(id);
        return CreatedAtAction(nameof(GetDeviceById), new { id }, created);
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
}