using UsersAPI.Models;
using UsersAPI.DTOs;

namespace UsersAPI.Services;

public class UserService
{
    private static List<User> users = new();

    public User Create(RegisterUserRequest request)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Role = "User"
        };

        users.Add(user);

        return user;
    }

    public List<User> GetAll()
    {
        return users;
    }

    public User? GetByEmail(string email)
    {
        return users.FirstOrDefault(x => x.Email == email);
    }

    public User? Update(Guid id, UpdateUserRequest request)
    {
        var user = users.FirstOrDefault(x => x.Id == id);

        if (user == null)
            return null;

        user.Name = request.Name;
        user.Email = request.Email;
        user.Password = request.Password;

        return user;
    }
}
