using Dapper;
using DeviceManagement.Api.Data;
using DeviceManagement.Api.Models;
using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Repositories.Interfaces;

namespace DeviceManagement.Api.Repositories;

public class DeviceRepository : IDeviceRepository
{
    public async Task<List<Device>> GetAllDevicesAsync()
    {
        using var connection = DatabaseConfig.GetDatabaseConnection();

        var query = "SELECT * FROM Devices";
        var devices = (await connection.QueryAsync<Device>(query)).ToList();

        return devices;
    }

    public async Task<Device> GetDeviceByIdAsync(int id)
    {
        using var connection = DatabaseConfig.GetDatabaseConnection();

        var query = "SELECT * FROM Devices WHERE id = @id";
        var device = await connection.QueryFirstOrDefaultAsync<Device>(query, new { id });

        return device;
    }

    public async Task AddDeviceAsync(DeviceDTO device)
    {
        using var connection = DatabaseConfig.GetDatabaseConnection();
        var query =
            "INSERT INTO Devices (name, d_type, os, os_version, processor, ram, description, current_user_id, manufacturer) VALUES (@Name, @Type, @OperatingSystem, @OsVersion, @Processor, @Ram, @Description, @UserId, @Manufacturer)";
        await connection.ExecuteAsync(query, device);
    }

    public async Task UpdateDeviceAsync(DeviceDTO device)
    {
        using var connection = DatabaseConfig.GetDatabaseConnection();
        var query =
            "UPDATE Devices SET name = @Name, d_type = @Type, os = @OperatingSystem, os_version = @OsVersion, processor = @Processor, ram = @Ram, description = @Description, current_user_id = @UserId, manufacturer = @Manufacturer WHERE id = @Id";
        await connection.ExecuteAsync(query, device);
    }

    public async Task DeleteDeviceAsync(int id)
    {
        using var connection = DatabaseConfig.GetDatabaseConnection();
        var query = "DELETE FROM Devices WHERE id = @id";
        await connection.ExecuteAsync(query, new { id });
    }
}