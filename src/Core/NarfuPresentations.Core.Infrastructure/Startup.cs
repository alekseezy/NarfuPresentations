using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NarfuPresentations.Core.Application.Common.DependencyInjection;
using NarfuPresentations.Core.Infrastructure;
using NarfuPresentations.Core.Infrastructure.Authentication;
using NarfuPresentations.Core.Infrastructure.Common.FileStorage;
using NarfuPresentations.Core.Infrastructure.Identity;
using NarfuPresentations.Core.Infrastructure.Middlewares;
using NarfuPresentations.Core.Infrastructure.OpenApi;
using NarfuPresentations.Core.Infrastructure.Persistense;
using NarfuPresentations.Core.Infrastructure.Persistense.Initialization;

using System.Reflection;

namespace NarfuPresentations.Core.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddApiVersioning()
            .AddIdentity(configuration)
            .AddAuthentication(configuration)
            .AddExceptionMiddleware()
            .AddHealthCheck()
            .AddMediatR(Assembly.GetExecutingAssembly())
            // FIX:
            //.AddNotifications(configuration)
            .AddOpenApiDocumentation(configuration)
            .AddPersistense(configuration)
            .AddRequestLogging(configuration)
            .AddRouting(options => options.LowercaseUrls = true)
            .AddServices();

    private static IServiceCollection AddApiVersioning(this IServiceCollection services) =>
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

    private static IServiceCollection AddHealthCheck(this IServiceCollection services) =>
        services.AddHealthChecks().Services;

    public static async Task InitializeDatabaseAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        var scope = services.CreateAsyncScope();

        await scope.ServiceProvider
            .GetRequiredService<IDatabaseInitializer>()
            .InitializeDatabaseAsync(cancellationToken);
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration configuration) =>
        app
            .UseStaticFiles()
            .UseFileStorage()
            .UseExceptionMiddleware()
            .UseRouting()
            .UseAuthentication()
            .UseCurrentUser()
            .UseAuthorization()
            .UseRequestLogging(configuration)
            .UseOpenApiDocumentation(configuration);

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapControllers().RequireAuthorization();
        builder.MapHealthChecks();
        // FIX:
        //builder.MapNotifications();

        return builder;
    }

    private static IEndpointConventionBuilder MapHealthChecks(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapHealthChecks("/api/health").RequireAuthorization();
}
