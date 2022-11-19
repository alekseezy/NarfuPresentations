using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NarfuPresentations.Presentation.MAUI.Common.Settings;
using NarfuPresentations.Presentation.MAUI.Models;
using NarfuPresentations.Presentation.MAUI.Pages;
using NarfuPresentations.Presentation.MAUI.Services;

namespace NarfuPresentations.Presentation.MAUI.ViewModels;

public sealed partial class SettingsViewModel : BaseViewModel
{
    private readonly IUserService _userService;

    public bool IsUserLoggedIn => _userService.IsLoggedIn;
    public bool IsUserNotLoggedIn => !IsUserLoggedIn;

    [ObservableProperty]
    private ApplicatonUser _user;

    public SettingsViewModel(IUserService userService)
    {
        _userService = userService;
        IsDarkModeEnabled = Theme.IsDarkThemeEnabled();
    }

    private bool isDarkModeEnabled;
    public bool IsDarkModeEnabled
    {
        get => isDarkModeEnabled;
        set
        {
            if (SetProperty(ref isDarkModeEnabled, value))
            {
                Settings.Theme = value
                    ? AppTheme.Dark
                    : AppTheme.Light;

                Theme.Set();
            }
        }
    }

    [RelayCommand]
    private async Task GoToLoginPageAsync()
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));

        OnPropertyChanged(nameof(IsUserLoggedIn));
        OnPropertyChanged(nameof(IsUserNotLoggedIn));
    }

    [RelayCommand]
    private async Task GoToLoginAppearancePageAsync()
    {
        await Shell.Current.GoToAsync(nameof(AppearanceSettingsPage), true, new Dictionary<string, object>
        {
            { nameof(SettingsViewModel), this }
        });
    }

    [RelayCommand]
    private async Task LogOutAsync()
    {
        if (IsUserNotLoggedIn)
            return;

        await _userService.LogoutAsync();

        OnPropertyChanged(nameof(IsUserLoggedIn));
        OnPropertyChanged(nameof(IsUserNotLoggedIn));
    }

    [RelayCommand]
    private async Task GoToMessagesAsync()
    {
        await Shell.Current.GoToAsync(nameof(MessagesPage), true);
    }

    [RelayCommand]
    private async Task GoToFavouritesAsync()
    {
        await Shell.Current.GoToAsync(nameof(FavouritesPage), true);
    }

    [RelayCommand]
    private async Task OnAppearingAsync()
    {
        if (IsUserLoggedIn)
            User = await _userService.GetUserAsync();

        OnPropertyChanged(nameof(IsUserLoggedIn));
        OnPropertyChanged(nameof(IsUserNotLoggedIn));
    }
}
