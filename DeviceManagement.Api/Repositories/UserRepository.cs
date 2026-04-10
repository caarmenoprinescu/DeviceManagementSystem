using Dapper;
using DeviceManagement.Api.Data;
using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Models;
using DeviceManagement.Api.Repositories.Interfaces;

namespace DeviceManagement.Api.Repositories;

public class UserRepository : IUserRepository
{
    public async Task<List<User>> GetAllUsersAsync()
    {
        using var connection = DatabaseConfig.GetDatabaseConnection();
        var query = "SELECT * FROM Users";
        var users = (await connection.QueryAsync<User>(query)).ToList();

        return users;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        using var connection = DatabaseConfig.GetDatabaseConnection();
        var query = "SELECT * FROM Users WHERE id = @id";
        var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { id });

        return user;
    }

    public async Task AddUserAsync(UserDTO user)
    {
        using var connection = DatabaseConfig.GetDatabaseConnection();
        var query = "INSERT INTO Users (name, role, location) VALUES (@Name, @Role, @Location)";
        await connection.ExecuteAsync(query, user);
    }

    public async Task UpdateUserAsync(int id, UserDTO user)
    {
        using var connection = DatabaseConfig.GetDatabaseConnection();
        var query = "UPDATE Users SET name = @Name, role = @Role, location = @Location WHERE id = @Id";
        await connection.ExecuteAsync(query, new
        {
            Id = id,
            user.Name,
            user.Role,
            user.Location
        });
    }

    public async Task DeleteUserAsync(int id)
    {
        using var connection = DatabaseConfig.GetDatabaseConnection();
        var query = "DELETE FROM Users WHERE id = @id";
        await connection.ExecuteAsync(query, new { id });
    }
}