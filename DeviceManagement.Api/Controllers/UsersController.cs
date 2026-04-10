using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] UserDTO user)
    {
        var id = await _userService.AddUserAsync(user);
        var created = await _userService.GetUserByIdAsync(id);
        return CreatedAtAction(nameof(GetUserById), new { id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserDTO user)
    {
        try
        {
            await _userService.UpdateUserAsync(id, user);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}