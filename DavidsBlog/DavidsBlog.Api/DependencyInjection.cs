using DavidsBlog.Api.Common.Errors;
using DavidsBlog.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DavidsBlog.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, DavidsBlogProblemDetailsFactory>();
            services.AddMappings();
            return services;
        }
    }
}