using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Models;

namespace DeviceManagement.Api.Repositories.Interfaces;

public interface IDeviceRepository
{
    Task<List<Device>> GetAllDevicesAsync();
    Task<Device?> GetDeviceByIdAsync(int id);
    Task<int> AddDeviceAsync(DeviceDTO device);
    Task<int> UpdateDeviceAsync(int id, DeviceDTO device);
    Task<int> DeleteDeviceAsync(int id);
}