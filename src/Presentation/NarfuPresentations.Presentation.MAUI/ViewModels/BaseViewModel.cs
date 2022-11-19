using CommunityToolkit.Mvvm.ComponentModel;

namespace NarfuPresentations.Presentation.MAUI.ViewModels;

public abstract partial class BaseViewModel : ObservableValidator
{
    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;

    public bool IsNotBusy => !_isBusy;
}
