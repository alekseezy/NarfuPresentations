using System.Reflection;

using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

using NarfuPresentations.Presentation.MAUI.API;
using NarfuPresentations.Presentation.MAUI.Common.Settings;
using NarfuPresentations.Presentation.MAUI.Common.Stores;
using NarfuPresentations.Presentation.MAUI.Pages;
using NarfuPresentations.Presentation.MAUI.Services;
using NarfuPresentations.Presentation.MAUI.ViewModels;

namespace NarfuPresentations.Presentation.MAUI;

[XamlCompilation(XamlCompilationOptions.Compile)]
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

//#if ANDROID && DEBUG
//        Platforms.Android.TrustProvider.DangerousAndroidMessageHandlerEmitter.Register();
//        Platforms.Android.TrustProvider.DangerousTrustProvider.Register();
//#endif

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMarkup()
            .ConfigureAnimations()
            .ConfigureEssentials()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Segoe-Ui-Bold.ttf", "SegoeUiBold");
                fonts.AddFont("Segoe-Ui-Regular.ttf", "SegoeUiRegular");
                fonts.AddFont("Segoe-Ui-Semibold.ttf", "SegoeUiSemibold");
                fonts.AddFont("Segoe-Ui-Semilight.ttf", "SegoeUiSemilight");
            });

        var configuration = builder.Configuration;

        var assembly = Assembly.GetExecutingAssembly();
        configuration.AddJsonFile(
            provider: new EmbeddedFileProvider(assembly, typeof(App).Namespace),
            path: "appsettings.json",
            optional: false,
            reloadOnChange: false);

        var services = builder.Services;
        ConfigureServices(services, configuration);

        RegisterRoutes();

        return builder.Build();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IUserService, DefaultUserService>();
        
        services.AddTransient<LoginViewModel>();
        services.AddTransient<RegisterViewModel>();
        services.AddTransient<EventViewModel>();
        services.AddTransient<EventDetailsViewModel>();

        services.AddSingleton<EventsViewModel>();
        services.AddSingleton<UserProfileViewModel>();
        services.AddSingleton<SettingsViewModel>();
        services.AddSingleton<MessagesViewModel>();
        services.AddSingleton<FavouritesViewModel>();
        
        services.AddTransient<LoginPage>();
        services.AddTransient<RegisterPage>();
        services.AddTransient<EventDetailsPage>();

        services.AddSingleton<ProfilePage>();
        services.AddSingleton<EventsPage>();
        services.AddSingleton<SettingsPage>();
        services.AddSingleton<MessagesPage>();
        services.AddSingleton<FavouritesPage>();

        services.AddApi(new TokenStore(), configuration);
    }

    private static void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(EventsPage), typeof(EventsPage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(EventDetailsPage), typeof(EventDetailsPage));
        Routing.RegisterRoute(nameof(FavouritesPage), typeof(FavouritesPage));
        Routing.RegisterRoute(nameof(MessagesPage), typeof(MessagesPage));
        Routing.RegisterRoute(nameof(DialogPage), typeof(DialogPage));
        Routing.RegisterRoute(nameof(AppearanceSettingsPage), typeof(AppearanceSettingsPage));
    }
}
