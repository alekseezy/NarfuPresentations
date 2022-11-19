using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NarfuPresentations.Presentation.MAUI.Pages;
using NarfuPresentations.Presentation.MAUI.Services;

namespace NarfuPresentations.Presentation.MAUI.ViewModels;

public partial class MessagesViewModel : ObservableObject
{
    public ObservableCollection<DialogViewModel> Messages { get; }

    [ObservableProperty]
    private bool _isRefreshing;
    private readonly IUserService _userService;

    public MessagesViewModel(IUserService userService)
    {
        Messages = new();
        RefreshMessagesAsync();
        _userService = userService;
    }

    [RelayCommand]
    private async Task OnAppearingAsync()
    {
        await Task.CompletedTask;
    }

    [RelayCommand]
    private async Task RefreshMessagesAsync()
    {
        IsRefreshing = true;

#if ANDROID
        if (!_userService.IsLoggedIn)
            return;


        var dialog1 = new DialogViewModel()
        {
            Sender = "Александр Никулин",
            RecievedOn = DateTime.Now.AddDays(-1),
            SenderAvatar = "grandpa_avatar_message.png"
        };
        dialog1.Messages.Add("Привет! Это мое первое сообщение!");

        var dialog2 = new DialogViewModel()
        {
            Sender = "Андрей Андреев",
            RecievedOn = DateTime.Now.AddHours(-7),
            SenderAvatar = "son_avatar_message.png"
        };
        dialog2.Messages.Add("Привет! Это мое первое сообщение!");

        Messages.Add(dialog1);
        Messages.Add(dialog2);
#else
        var dialog1 = new DialogViewModel()
        {
            Sender = "Дмитрий Синичкин",
            RecievedOn = DateTime.Now.AddDays(-1),
            SenderAvatar = "grandpa_avatar_message.png"
        };
        dialog1.Messages.Add("Привет! Это мое первое сообщение!");

        var dialog2 = new DialogViewModel()
        {
            Sender = "Олег Галкин",
            RecievedOn = DateTime.Now.AddHours(-7),
            SenderAvatar = "son_avatar_message.png"
        };
        dialog2.Messages.Add("Привет! Это мое первое сообщение!");

        Messages.Add(dialog1);
        Messages.Add(dialog2);
#endif
        IsRefreshing = false;

        await Task.CompletedTask;
    }

    [RelayCommand]
    private async Task OpenDialogAsync(DialogViewModel dialog)
    {
        await Shell.Current.GoToAsync(nameof(DialogPage), true, new Dictionary<string, object>
        {
            { "Dialog", dialog }
        });
    }
}
