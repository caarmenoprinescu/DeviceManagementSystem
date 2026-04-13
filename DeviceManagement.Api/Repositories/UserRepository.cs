using Dapper;
using DeviceManagement.Api.Data.Interfaces;
using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Models;
using DeviceManagement.Api.Repositories.Interfaces;

namespace DeviceManagement.Api.Repositories;

public class UserRepository(IDbConnectionFactory dbConfig) : IUserRepository
{
    private readonly IDbConnectionFactory _dbConfig = dbConfig;


    public async Task<List<User>> GetAllUsersAsync()
    {
        using var connection = _dbConfig.CreateConnection();
        var query = "SELECT * FROM Users";
        var users = (await connection.QueryAsync<User>(query)).ToList();

        return users;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        using var connection = _dbConfig.CreateConnection();
        var query = "SELECT id, name, role, location, email, password_hash AS PasswordHash FROM Users WHERE id = @id";
        var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { id });

        return user;
    }
    
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        using var connection = _dbConfig.CreateConnection();
        var query = "SELECT id, name,role,location,email,password_hash AS PasswordHash FROM Users WHERE email = @email";
        var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { email });

        return user;
    }


    public async Task<int> AddUserAsync(UserDTO user)
    {
        using var connection = _dbConfig.CreateConnection();
        var query =
            "INSERT INTO Users (name, role, location) VALUES (@Name, @Role, @Location); Select SCOPE_IDENTITY();";
        var newId = await connection.ExecuteScalarAsync<int>(query, user);
        return newId;
    }

    public async Task<int> UpdateUserAsync(int id, UserDTO user)
    {
        using var connection = _dbConfig.CreateConnection();
        var query = "UPDATE Users SET name = @Name, role = @Role, location = @Location WHERE id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, new
        {
            Id = id,
            user.Name,
            user.Role,
            user.Location
        });
        return rowsAffected;
    }

    public async Task<int> DeleteUserAsync(int id)
    {
        using var connection = _dbConfig.CreateConnection();
        var query = "DELETE FROM Users WHERE id = @id";
        var rowsAffected = await connection.ExecuteAsync(query, new { id });
        return rowsAffected;
    }

    public async Task<int> AddAuthUserAsync(User user)
    {
        using var connection = _dbConfig.CreateConnection();
        var query =
            "INSERT INTO Users (name, role, location, email, password_hash) VALUES (@Name, @Role, @Location, @Email, @PasswordHash); Select SCOPE_IDENTITY();";
        var newId = await connection.ExecuteScalarAsync<int>(query, user);
        return newId;
    }
}