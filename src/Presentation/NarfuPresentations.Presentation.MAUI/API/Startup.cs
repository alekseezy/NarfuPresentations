using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NarfuPresentations.Presentation.MAUI.API.Http.DelegatingHandlers;
using NarfuPresentations.Presentation.MAUI.API.Http.Security;
using NarfuPresentations.Presentation.MAUI.API.ServerAPI.Events;
using NarfuPresentations.Presentation.MAUI.API.ServerAPI.Identity;
using NarfuPresentations.Presentation.MAUI.API.ServerAPI.Personal;
using NarfuPresentations.Presentation.MAUI.API.ServerAPI.Settings;
using NarfuPresentations.Presentation.MAUI.API.ServerAPI.Stores.Contracts;

using Refit;

namespace NarfuPresentations.Presentation.MAUI.API;

public static class Startup
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        IAuthenticationTokenStore authenticationTokenStore,
        IConfiguration configuration)
    {
        var apiSettings = configuration.GetSection(nameof(APISettings)).Get<APISettings>();

        services.AddTransient(_ => authenticationTokenStore);
        services.AddTransient<AuthenticationHeaderHandler>();

        services
            .AddRefitClient<IUsersApi>()
            .ConfigureHttpClient(options => options.BaseAddress = new Uri(apiSettings.VersionNeutralEndpoint))
            .ConfigureClientHandler(apiSettings);

        services
            .AddRefitClient<ITokensApi>()
            .ConfigureHttpClient(options => options.BaseAddress = new Uri(apiSettings.VersionNeutralEndpoint))
            .ConfigureClientHandler(apiSettings);

        services
            .AddRefitClient<IPersonalApi>()
            .ConfigureHttpClient(options => options.BaseAddress = new Uri(apiSettings.VersionNeutralEndpoint))
            .AddHttpMessageHandler<AuthenticationHeaderHandler>()
            .ConfigureClientHandler(apiSettings);

        services
            .AddRefitClient<IEventsApi>()
            .ConfigureHttpClient(options => options.BaseAddress = new Uri(apiSettings.VersionedEndpoint))
            .AddHttpMessageHandler<AuthenticationHeaderHandler>()
            .ConfigureClientHandler(apiSettings);

        return services;
    }

    private static IHttpClientBuilder ConfigureClientHandler(this IHttpClientBuilder builder, APISettings settings) =>
        builder.ConfigurePrimaryHttpMessageHandler(() =>
        {
            if (settings.UseCustomCN)
                return CommonHandlers.GetCustomCNClientHandler(settings);

            if (settings.UseInsecureCertificate)
                return CommonHandlers.GetInsecureClientHandler();

            return new HttpClientHandler();
        });
}
