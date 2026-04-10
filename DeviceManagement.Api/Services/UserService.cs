using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Models;
using DeviceManagement.Api.Repositories.Interfaces;
using DeviceManagement.Api.Services.Interfaces;

namespace DeviceManagement.Api.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<int> AddUserAsync(UserDTO user)
    {
       return await _userRepository.AddUserAsync(user);
    }

    public async Task UpdateUserAsync(int id,UserDTO user)
    {
      var rows = await  _userRepository.UpdateUserAsync(id,user);
      if(rows == 0)
          throw new KeyNotFoundException($"User with id {id} not found");
  
    }

    public async Task DeleteUserAsync(int id)
    { 
        var rows = await  _userRepository.DeleteUserAsync(id);
        if(rows == 0)
            throw new KeyNotFoundException($"User with id {id} not found");
        
    }
}