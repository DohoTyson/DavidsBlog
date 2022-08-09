using DavidsBlog.Application.Common.Interfaces.Authentication;
using DavidsBlog.Application.Common.Interfaces.Services;
using DavidsBlog.Infrastructure.Authentication;
using DavidsBlog.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace DavidsBlog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection collection,
        ConfigurationManager configuration)
    {
        collection.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        collection.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        collection.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return collection;
    }
}