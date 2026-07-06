using Microsoft.AspNetCore.Mvc;
using TaskApi_DotNet.DTOs;
using TaskApi_DotNet.Services;

namespace TaskApi_DotNet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto dto)
    {
        bool success = await _userService.Register(dto);

        if (!success)
            return BadRequest("Email already exists.");

        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        bool success = await _userService.Login(dto);

        if (!success)
            return Unauthorized("Invalid email or password.");

        return Ok("Login successful.");
    }
}