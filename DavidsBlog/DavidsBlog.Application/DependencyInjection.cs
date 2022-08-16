using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DavidsBlog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddMediatR(typeof(DependencyInjection).Assembly);
        return collection;
    }
}