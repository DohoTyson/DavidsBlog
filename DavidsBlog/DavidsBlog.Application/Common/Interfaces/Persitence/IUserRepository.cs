using DavidsBlog.Domain.Entities;

namespace DavidsBlog.Application.Common.Interfaces.Persitence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);

    void AddNewUser(User user);
}
