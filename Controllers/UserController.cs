using Microsoft.AspNetCore.Mvc;
using TaskApi_DotNet.DTOs;
using TaskApi_DotNet.Services;

namespace TaskApi_DotNet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly JwtService _jwtService;

    public UsersController(UserService userService , JwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
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
        var user = await _userService.Login(dto);

        if (user == null)
            return Unauthorized("Invalid email or password.");

        var token = _jwtService.GenerateToken(user);

        return Ok(new
        {
            token = token
        });
    }
}