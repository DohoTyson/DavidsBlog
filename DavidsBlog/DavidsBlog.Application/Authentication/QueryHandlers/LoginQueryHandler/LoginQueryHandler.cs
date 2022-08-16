using DavidsBlog.Application.Authentication.Commands.Register;
using DavidsBlog.Application.Authentication.Commands.Register.Queries.Login;
using DavidsBlog.Application.Authentication.Common;
using DavidsBlog.Application.Common.Interfaces.Authentication;
using DavidsBlog.Application.Common.Interfaces.Persitence;
using DavidsBlog.Domain.Common.Errors;
using DavidsBlog.Domain.Entities;
using ErrorOr;
using MediatR;

namespace DavidsBlog.Application.Authentication.QueryHandlers.LoginQueryHandler;

public class LoginQueryHandler : 
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (user.Password != query.Password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}
