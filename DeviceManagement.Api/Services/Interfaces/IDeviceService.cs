using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Models;

namespace DeviceManagement.Api.Services.Interfaces;

public interface IDeviceService
{
    Task<List<Device>> GetAllDevicesAsync();
    Task<Device?> GetDeviceByIdAsync(int id);
    Task<int> AddDeviceAsync(DeviceDTO device);
    Task UpdateDeviceAsync(int id,DeviceDTO device);
    Task DeleteDeviceAsync(int id);
}