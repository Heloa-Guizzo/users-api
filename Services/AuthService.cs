using UsersAPI.DTOs;

namespace UsersAPI.Services;

public class AuthService
{
    private readonly UserService _userService;

    public AuthService(UserService userService)
    {
        _userService = userService;
    }

    public bool Login(LoginRequest request)
    {
        var user = _userService.GetByEmail(request.Email);

        if (user == null)
            return false;

        if (user.Password != request.Password)
            return false;

        return true;
    }
}
