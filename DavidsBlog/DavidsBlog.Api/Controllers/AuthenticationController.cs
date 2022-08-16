using DavidsBlog.Application.Authentication.Commands.Register;
using DavidsBlog.Application.Authentication.Commands.Register.Queries.Login;
using DavidsBlog.Application.Authentication.Common;
using DavidsBlog.Contracts.Authentication;
using DavidsBlog.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DavidsBlog.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender mediatorSender;
    private readonly IMapper mapper;

    public AuthenticationController(
        ISender mediatorSender,
        IMapper mapper)
    {
        this.mediatorSender = mediatorSender ?? throw new ArgumentNullException(nameof(mediatorSender));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var registerCommand = this.mapper.Map<RegisterCommand>(request);
        ErrorOr<AuthenticationResult> authResult = await this.mediatorSender.Send(registerCommand);

        return authResult.Match(
            authResult => Ok(this.mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var loginQuery = this.mapper.Map<LoginQuery>(request);
        ErrorOr<AuthenticationResult> authResult = await this.mediatorSender.Send(loginQuery);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description
            );
        }

        return authResult.Match(
            authResult => Ok(this.mapper.Map<AuthenticationResponse>(authResult)),
            error => Problem(error)
        );
    }
}