using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Models;

namespace DeviceManagement.Api.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<User>>GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task AddUserAsync(UserDTO user);
    Task UpdateUserAsync(UserDTO user);
    Task DeleteUserAsync(int id);
 
    
    
}