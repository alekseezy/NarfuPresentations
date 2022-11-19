using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NarfuPresentations.Presentation.MAUI.Models;
using NarfuPresentations.Presentation.MAUI.Services;

using Refit;

namespace NarfuPresentations.Presentation.MAUI.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    private readonly IUserService _userService;

    [ObservableProperty]
    private ApplicatonUser _user = new();

    public RegisterViewModel(IUserService userService)
    {
        _userService = userService;
    }

    [RelayCommand]
    private async Task RegisterAsync(CancellationToken cancellationToken)
    {
        try
        {
            IsBusy = true;
            await _userService.RegisterAsync(_user, cancellationToken);

            if (_userService.IsLoggedIn)
                await Shell.Current.GoToAsync("../..");
            else
                await Shell.Current.GoToAsync("..");
        }
        catch(ValidationApiException validationException)
        {
            await Shell.Current.DisplayAlert("Ошибка регистрации", "Проверьте правильность/полноту заполнения данных и повторите попытку.", "ОК");
        }
        catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка регистрации", "Произошла ошибка, попробуйте повторить позже.", "ОК");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
