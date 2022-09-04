using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NarfuPresentations.Core.Application.Identity;
using NarfuPresentations.Core.Infrastructure.Authentication.JWT;
using NarfuPresentations.Core.Infrastructure.Authentication.Middlewares;
using NarfuPresentations.Core.Infrastructure.Authentication.Permissions;

namespace NarfuPresentations.Core.Infrastructure.Authentication;

internal static class Startup
{
    internal static IServiceCollection AddAuthentication(this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddCurrentUser()
            .AddPermissions()
            .AddJwtAuthentication(configuration);

    internal static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app) =>
        app.UseMiddleware<CurrentUserMiddleware>();

    private static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
        services
            .AddScoped<CurrentUserMiddleware>()
            .AddScoped<ICurrentUser, CurrentUser>()
            .AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>());

    private static IServiceCollection AddPermissions(this IServiceCollection services) =>
        services
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
            .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
}
