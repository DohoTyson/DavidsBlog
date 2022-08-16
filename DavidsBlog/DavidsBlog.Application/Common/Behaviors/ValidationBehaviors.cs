using ErrorOr;
using FluentValidation;
using MediatR;

namespace DavidsBlog.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
{
    private readonly IValidator<TRequest> validator;

    public ValidationBehavior(IValidator<TRequest> validator)
    {
        this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var validationResult = await this.validator.ValidateAsync(request);

        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors
            .ConvertAll(e => Error.Validation(e.PropertyName, e.ErrorMessage));

        return (dynamic)errors;
    }
}