using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NarfuPresentations.Presentation.MAUI.API.ServerAPI.Personal;
using NarfuPresentations.Shared.Contracts.Identity.Users.Responses;

namespace NarfuPresentations.Presentation.MAUI.ViewModels;

public sealed partial class UserProfileViewModel : BaseViewModel
{
    private readonly IPersonalApi _personalApi;

    [ObservableProperty]
    private UserDetailsResponse _data;

    public UserProfileViewModel(IPersonalApi personalApi)
    {
        _personalApi = personalApi;
    }

    [RelayCommand]
    private async Task LoadData(CancellationToken cancellationToken)
    {
        var data = await _personalApi.GetProfileAsync(cancellationToken);
        _data = data.Content;
    }
}
