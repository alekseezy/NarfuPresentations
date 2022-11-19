using System.ComponentModel.DataAnnotations;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NarfuPresentations.Presentation.MAUI.Pages;
using NarfuPresentations.Presentation.MAUI.Services;
using NarfuPresentations.Shared.Contracts.Identity.Tokens.Requests;

namespace NarfuPresentations.Presentation.MAUI.ViewModels;

public sealed partial class LoginViewModel : BaseViewModel
{
    private readonly IUserService _userService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    [Required]
    [MinLength(8)]
    [MaxLength(70)]
    [EmailAddress]
    private string _email;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    [Required]
    [MinLength(8)]
    [MaxLength(70)]
    private string _password;

    public LoginViewModel(IUserService userService) =>
        _userService = userService;

    [RelayCommand(AllowConcurrentExecutions = false, CanExecute = nameof(CanLogin))]
    private async Task LoginAsync(CancellationToken cancellationToken)
    {
        await _userService.LoginAsync(Email, Password, cancellationToken);

        if (_userService.IsLoggedIn)
            await Shell.Current.GoToAsync("..");
    }

    private bool CanLogin()
    {
        ValidateAllProperties();

        return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
    }

    [RelayCommand]
    private async Task GoToRegisterPageAsync() =>
        await Shell.Current.GoToAsync(nameof(RegisterPage));
}
