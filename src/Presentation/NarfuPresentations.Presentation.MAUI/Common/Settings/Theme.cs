namespace NarfuPresentations.Presentation.MAUI.Common.Settings;

internal static class Theme
{
    internal static AppTheme Get() => Application.Current.UserAppTheme = Settings.Theme switch
    {
        AppTheme.Dark => AppTheme.Dark,
        AppTheme.Unspecified => AppTheme.Unspecified,
        _ => AppTheme.Light,
    };

    internal static bool IsDarkThemeEnabled() =>
        Application.Current.UserAppTheme == AppTheme.Dark;

    internal static void Set()
    {
        Application.Current.UserAppTheme = Settings.Theme switch
        {
            AppTheme.Dark => AppTheme.Dark,
            AppTheme.Unspecified => AppTheme.Unspecified,
            _ => AppTheme.Light,
        };
    }
}
