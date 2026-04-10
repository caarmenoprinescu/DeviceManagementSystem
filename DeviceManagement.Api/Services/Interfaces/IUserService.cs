using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Models;

namespace DeviceManagement.Api.Services.Interfaces;

public interface IUserService
{
    Task<List<User>>GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<int> AddUserAsync(UserDTO user);
    Task UpdateUserAsync(int id, UserDTO user);
    Task DeleteUserAsync(int id);

    
}