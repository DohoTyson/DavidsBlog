using DavidsBlog.Application.Common.Interfaces.Persitence;
using DavidsBlog.Domain.Entities;

namespace DavidsBlog.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> users = new List<User>();

    public void AddNewUser(User user)
    {
        users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return users.SingleOrDefault(u => u.Email == email);
    }
}
