using Microsoft.AspNetCore.Mvc;
using UsersAPI.DTOs;
using UsersAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace UsersAPI.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AuthService _authService;
    private readonly JwtService _jwtService;

    public AuthController(UserService userService, AuthService authService, JwtService jwtService)
    {
        _userService = userService;
        _authService = authService;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterUserRequest request)
    {
        var user = _userService.Create(request);

        return Created("", new
        {
            user.Id,
            user.Name,
            user.Email
        });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _userService.GetByEmail(request.Email);

        if (user == null || user.Password != request.Password)
            return Unauthorized();

        var token = _jwtService.GenerateToken(user);

        return Ok(new { token });
    }
}

