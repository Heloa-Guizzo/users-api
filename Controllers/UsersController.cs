using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.DTOs;
using UsersAPI.Services;

namespace UsersAPI.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
        var users = _userService.GetAll();

        return Ok(users);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] UpdateUserRequest request)
    {
        var user = _userService.Update(id, request);

        if (user == null)
            return NotFound();

        return Ok(new
        {
            user.Id,
            user.Name,
            user.Email
        });
    }
}
