using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Models;

namespace DeviceManagement.Api.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<User>>GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<int> AddUserAsync(UserDTO user);
    Task<int> UpdateUserAsync(int id, UserDTO user);
    Task<int> DeleteUserAsync(int id);
 
    
    
}