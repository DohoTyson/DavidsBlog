using System.Reflection;
using DavidsBlog.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DavidsBlog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddMediatR(typeof(DependencyInjection).Assembly);
        collection.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        collection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return collection;
    }
}