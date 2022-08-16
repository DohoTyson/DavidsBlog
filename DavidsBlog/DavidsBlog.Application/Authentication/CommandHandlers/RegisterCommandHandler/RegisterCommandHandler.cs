using DavidsBlog.Application.Authentication.Commands.Register;
using DavidsBlog.Application.Authentication.Common;
using DavidsBlog.Application.Common.Interfaces.Authentication;
using DavidsBlog.Application.Common.Interfaces.Persitence;
using DavidsBlog.Domain.Common.Errors;
using DavidsBlog.Domain.Entities;
using ErrorOr;
using MediatR;

namespace DavidsBlog.Application.Authentication.CommandHandlers.RegisterCommandHandler;

public class RegisterCommandHandler : 
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Check if user already exists
        if (userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        // Create user (generate unique ID)
        var newUser = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };

        userRepository.AddNewUser(newUser);

        // Create JWT 
        var token = jwtTokenGenerator.GenerateToken(newUser);

        return new AuthenticationResult(
            newUser,
            token);
    }
}