using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Models;
using DeviceManagement.Api.Repositories.Interfaces;
using DeviceManagement.Api.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace DeviceManagement.Api.Services;

public class DeviceService(IDeviceRepository deviceRepository) : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository = deviceRepository;

    public async Task<List<Device>> GetAllDevicesAsync()
    {
        return await _deviceRepository.GetAllDevicesAsync();
    }

    public async Task<Device?> GetDeviceByIdAsync(int id)
    {
        return await _deviceRepository.GetDeviceByIdAsync(id);
    }

    public async Task<int> AddDeviceAsync(DeviceDTO device)
    {
        try
        {
            return await _deviceRepository.AddDeviceAsync(device);
        }
        catch (SqlException ex) when (ex.Number == 2627)
        {
            throw new Exception("Device already exists");
        }
    }

    public async Task UpdateDeviceAsync(int id,DeviceDTO device)
    {
        var rows = await  _deviceRepository.UpdateDeviceAsync(id,device);
        if(rows == 0)
            throw new KeyNotFoundException($"Device with id {id} not found");
  

    }

    public async Task DeleteDeviceAsync(int id)
    {
        var rows = await _deviceRepository.DeleteDeviceAsync(id);
        if (rows == 0)
            throw new KeyNotFoundException($"Device with id {id} not found");
         
    }
}