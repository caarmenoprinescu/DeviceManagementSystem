using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Models;
using DeviceManagement.Api.Repositories.Interfaces;

namespace DeviceManagement.Api.Services.Interfaces;
public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
       return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task AddUserAsync(UserDTO user)
    {
        await _userRepository.AddUserAsync(user);
    }

    public async Task UpdateUserAsync(UserDTO user)
    {
        await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteUserAsync(id);
    }
}