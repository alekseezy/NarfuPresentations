using CommunityToolkit.Mvvm.ComponentModel;

using NarfuPresentations.Shared.Domain.Entities;

namespace NarfuPresentations.Presentation.MAUI.ViewModels;

public sealed partial class EventViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _image;
    [ObservableProperty]
    private AccessType _accessType;
    [ObservableProperty]
    private string _description;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(StartsOnDateMonth))]
    private DateTime _startsOn;

    public string StartsOnDateMonth => string.Format("{0:dd MMM}", _startsOn);
    public string StartsOnDateMonthYear => string.Format("{0:dd MMMM yyyy}", _startsOn);
    public string StartsOnTime => string.Format("{0:HH:mm}", _startsOn);

    public EventViewModel()
    {

    }

    public override string ToString()
    {
        return Title;
    }
}
