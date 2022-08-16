using DavidsBlog.Application.Authentication.Commands.Register.Queries.Login;
using FluentValidation;

namespace DavidsBlog.Application.Authentication.QueryValidator;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}