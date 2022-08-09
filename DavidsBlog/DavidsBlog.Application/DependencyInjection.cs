using DavidsBlog.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace DavidsBlog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IAuthenticationService, AuthenticationService>();
        return collection;
    }
}