using DeviceManagement.Api.DTOs;
using DeviceManagement.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO register)
    {
        try
        {
            var registered = await _authService.Register(register);
            return Created("", registered);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO login)
    {
        try
        {
            var loggedIn = await _authService.Login(login);
            return Ok(loggedIn);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}