using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

namespace NarfuPresentations.Presentation.MAUI.ViewModels;

public partial class DialogViewModel : ObservableObject
{
    public ObservableCollection<string> Messages { get; } = new();
    public string LastMessage => Messages[^1];

    [ObservableProperty]
    private string _sender;
    [ObservableProperty]
    private DateTime _recievedOn;
    [ObservableProperty]
    private string _senderAvatar;
}
