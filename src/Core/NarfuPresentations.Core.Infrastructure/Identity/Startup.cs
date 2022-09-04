using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NarfuPresentations.Core.Infrastructure.Identity.Models;
using NarfuPresentations.Core.Infrastructure.Identity.Settings;
using NarfuPresentations.Core.Infrastructure.Persistence.Context;

namespace NarfuPresentations.Core.Infrastructure.Identity;

internal static class Startup
{
    internal static IServiceCollection AddIdentity(this IServiceCollection services,
        IConfiguration configuration)
    {
        var passwordSettings =
            configuration.GetSection(nameof(PasswordSettings)).Get<PasswordSettings>();

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = passwordSettings.PasswordLength;
                options.Password.RequireDigit = passwordSettings.RequireDigit;
                options.Password.RequireLowercase = passwordSettings.RequireLowercase;
                options.Password.RequireUppercase = passwordSettings.RequireUppercase;
                options.Password.RequireNonAlphanumeric = passwordSettings.RequireNonAlphanumeric;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
