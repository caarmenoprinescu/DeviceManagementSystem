using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Models;

namespace DeviceManagement.Api.Repositories.Interfaces;

public interface IDeviceRepository
{
    Task<List<Device>> GetAllDevicesAsync();
    Task<Device> GetDeviceByIdAsync(int id);
    Task AddDeviceAsync(DeviceDTO device);
    Task UpdateDeviceAsync(DeviceDTO device);
    Task DeleteDeviceAsync(int id);
}