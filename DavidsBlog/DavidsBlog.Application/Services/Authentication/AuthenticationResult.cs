namespace DavidsBlog.Application.Services.Authentication;

public record AuthenticationResult(
    Guid Id,
    string FirstName,
    string Lastname,
    string Email,
    string Token);
