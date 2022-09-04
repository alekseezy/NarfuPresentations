using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace NarfuPresentations.Core.Infrastructure.SecurityHeaders;

internal static class Startup
{
    internal static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app,
        IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(SecurityHeaderSettings))
            .Get<SecurityHeaderSettings>();

        if (settings?.Enable is not true)
            return app;

        app.Use(async (context, next) =>
        {
            if (context.Response.HasStarted)
                await next();

            if (!string.IsNullOrWhiteSpace(settings.XFrameOptions))
                context.Response.Headers.Add(HeaderNames.XFRAMEOPTIONS, settings.XFrameOptions);

            if (!string.IsNullOrWhiteSpace(settings.XContentTypeOptions))
                context.Response.Headers.Add(HeaderNames.XCONTENTTYPEOPTIONS,
                    settings.XContentTypeOptions);

            if (!string.IsNullOrWhiteSpace(settings.ReferrerPolicy))
                context.Response.Headers.Add(HeaderNames.REFERRERPOLICY, settings.ReferrerPolicy);

            if (!string.IsNullOrWhiteSpace(settings.PermissionsPolicy))
                context.Response.Headers.Add(HeaderNames.PERMISSIONSPOLICY,
                    settings.PermissionsPolicy);

            if (!string.IsNullOrWhiteSpace(settings.SameSite))
                context.Response.Headers.Add(HeaderNames.SAMESITE, settings.SameSite);

            if (!string.IsNullOrWhiteSpace(settings.XXSSProtection))
                context.Response.Headers.Add(HeaderNames.XXSSPROTECTION, settings.XXSSProtection);
        });

        return app;
    }
}
