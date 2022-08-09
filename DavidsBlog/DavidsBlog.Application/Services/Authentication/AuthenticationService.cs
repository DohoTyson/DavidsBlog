using DavidsBlog.Application.Common.Interfaces.Authentication;

namespace DavidsBlog.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(),
            "FirstName",
            "LastName",
            email,
            "token");
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // Check if user already exists

        // Create user (generate unique ID)

        // Create JWT 
        var userId = Guid.NewGuid();
        var token = this.jwtTokenGenerator.GenerateToken(userId, firstName, lastName);

        return new AuthenticationResult(
            userId,
            firstName,
            lastName,
            email,
            token);
    }
}
