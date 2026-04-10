using Dapper;
using DeviceManagement.Api.Data;
using DeviceManagement.Api.Data.Interfaces;
using DeviceManagement.Api.Models;
using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Repositories.Interfaces;

namespace DeviceManagement.Api.Repositories;

public class DeviceRepository(IDbConnectionFactory dbConfig) : IDeviceRepository
{
    private readonly IDbConnectionFactory _dbConfig = dbConfig;

    private const string SelectQuery =
        "SELECT id, name, d_type AS Type, os AS OperatingSystem, os_version AS OsVersion, processor, ram, description, current_user_id AS UserId, manufacturer FROM Devices";

    public async Task<List<Device>> GetAllDevicesAsync()
    {
        using var connection = _dbConfig.CreateConnection();

        var devices = (await connection.QueryAsync<Device>(SelectQuery)).ToList();

        return devices;
    }

    public async Task<Device?> GetDeviceByIdAsync(int id)
    {
        using var connection = _dbConfig.CreateConnection();

        var query = SelectQuery + " WHERE id = @id";
        var device = await connection.QueryFirstOrDefaultAsync<Device>(query, new { id });
        return device;
    }

    public async Task<int> AddDeviceAsync(DeviceDTO device)
    {
        using var connection = _dbConfig.CreateConnection();
        var query =
            "INSERT INTO Devices (name, d_type, os, os_version, processor, ram, description, current_user_id, manufacturer) VALUES (@Name, @Type, @OperatingSystem, @OsVersion, @Processor, @Ram, @Description, @UserId, @Manufacturer); SELECT SCOPE_IDENTITY();";
        var newId = await connection.ExecuteScalarAsync<int>(query, device);
        return newId;
    }

    public async Task<int> UpdateDeviceAsync(int id, DeviceDTO device)
    {
        using var connection = _dbConfig.CreateConnection();
        var query =
            "UPDATE Devices SET name = @Name, d_type = @Type, os = @OperatingSystem, os_version = @OsVersion, processor = @Processor, ram = @Ram, description = @Description, current_user_id = @UserId, manufacturer = @Manufacturer WHERE id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, new
        {
            Id = id,
            device.Name,
            device.Type,
            device.OperatingSystem,
            device.OsVersion,
            device.Processor,
            device.Ram,
            device.Description,
            device.UserId,
            device.Manufacturer
        });
        return rowsAffected;
    }

    public async Task<int> DeleteDeviceAsync(int id)
    {
        using var connection = _dbConfig.CreateConnection();
        var query = "DELETE FROM Devices WHERE id = @id";
        var rowsAffected = await connection.ExecuteAsync(query, new { id });
        return rowsAffected;
    }
}