using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NarfuPresentations.Core.Infrastructure.Middlewares.Settings;

namespace NarfuPresentations.Core.Infrastructure.Middlewares;

internal static class Startup
{
    internal static IServiceCollection AddExceptionMiddleware(this IServiceCollection services) =>
        services.AddScoped<ExceptionMiddleware>();

    internal static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app) =>
        app.UseMiddleware<ExceptionMiddleware>();

    internal static IServiceCollection AddRequestLogging(this IServiceCollection services, IConfiguration configuration)
    {
        if (!GetMiddlewareSettings(configuration).EnableHttpsLogging)
            return services;

        services.AddSingleton<RequestLoggingMiddleware>();
        services.AddScoped<ResponseLoggingMiddleware>();

        return services;
    }

    internal static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app, IConfiguration configuration)
    {
        if (!GetMiddlewareSettings(configuration).EnableHttpsLogging)
            return app;

        app.UseMiddleware<RequestLoggingMiddleware>();
        app.UseMiddleware<ResponseLoggingMiddleware>();

        return app;
    }

    private static MiddlewareSettings GetMiddlewareSettings(IConfiguration configuration) =>
        configuration.GetSection(nameof(MiddlewareSettings)).Get<MiddlewareSettings>();
}
