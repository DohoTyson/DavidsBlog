using DavidsBlog.Domain.Entities;

namespace DavidsBlog.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);
