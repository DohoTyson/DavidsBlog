using DavidsBlog.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace DavidsBlog.Application.Authentication.Commands.Register.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;