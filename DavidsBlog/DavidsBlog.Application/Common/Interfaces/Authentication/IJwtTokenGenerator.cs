using DavidsBlog.Domain.Entities;

namespace DavidsBlog.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}