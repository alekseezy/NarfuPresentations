using NarfuPresentations.Presentation.MAUI.Common.Settings;
using NarfuPresentations.Presentation.MAUI.Shells;

namespace NarfuPresentations.Presentation.MAUI;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        if (RuntimeConfiguration.IsDesktop)
            MainPage = new DesktopShell();
        else
            MainPage = new MobileShell();

        RequestedThemeChanged += (s, a) =>
        {
            Settings.Theme = a.RequestedTheme;
            Theme.Set();
        };
    }
}
