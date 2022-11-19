using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NarfuPresentations.Presentation.MAUI.Pages;

namespace NarfuPresentations.Presentation.MAUI.ViewModels;

public partial class FavouritesViewModel : ObservableObject
{
    private const string _imageSource = "https://img2.looper.com/img/gallery/demolition-man-2-release-date-cast-and-plot-what-we-know-so-far/l-intro-1620519286.jpg";

    public ObservableCollection<EventViewModel> Favourites { get; } = new();

    [ObservableProperty]
    private bool _isRefreshing;

    public FavouritesViewModel()
    {
        RefreshFavouritesAsync();
    }

    [RelayCommand(AllowConcurrentExecutions = false)]
    private async Task GoToEventDetails(EventViewModel @event)
    {
        if (@event is null)
            return;

        try
        {
            await Shell.Current.GoToAsync(nameof(EventDetailsPage), true, new Dictionary<string, object>
            {
                {"Event", @event }
            });
        }
        catch (Exception ex)
        {

        }
    }

    [RelayCommand]
    private async Task RefreshFavouritesAsync()
    {
        IsRefreshing = true;
        var stanlone = @"https://img2.looper.com/img/gallery/demolition-man-2-release-date-cast-and-plot-what-we-know-so-far/l-intro-1620519286.jpg";
        var zhackFresko = @"https://memepedia.ru/wp-content/uploads/2020/02/zhak-fresko-citaty-mem.png";
        var albertEinshtein = @"https://hi-news.ru/wp-content/uploads/2020/07/albert_einstein-750x500.jpg";
        var diesel = @"https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/Vin_Diesel_by_Gage_Skidmore_2.jpg/274px-Vin_Diesel_by_Gage_Skidmore_2.jpg";
        var gordonRamzi = @"https://www.vokrug.tv/pic/person/f/9/3/3/f933763ca80553d7f2cddba7e969c021.jpg";
#if ANDROID
        var events = new List<EventViewModel>
        {
            new()
            {
                Title = "Актерское мастерство",
                Image = _imageSource,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris eu est semper, fermentum dui ac, scelerisque ipsum. Proin imperdiet velit ut vestibulum tincidunt. Etiam venenatis turpis nec urna lacinia, sed tincidunt est scelerisque. Sed lobortis ac sem ultricies feugiat. Nunc sapien dolor, hendrerit at tempor non, maximus ut eros. Quisque massa tellus, porttitor ac elit ut, pulvinar dictum mauris. Morbi eget tincidunt dui. In venenatis nisl non neque porttitor, at semper magna viverra. Pellentesque ornare leo vitae sagittis accumsan. Morbi venenatis maximus libero a faucibus. Phasellus quis quam orci. Vivamus dolor nulla, congue sit amet hendrerit sed, luctus gravida tortor. Integer aliquet ipsum ac iaculis sollicitudin.\r\n\r\nInteger convallis luctus tristique. Nam posuere, velit ut mattis rutrum, mi magna congue purus, nec ullamcorper orci orci a velit. Integer non dictum augue. Suspendisse sed sollicitudin quam. In fermentum mollis libero, ut fringilla lacus viverra et. Quisque nisi leo, lacinia vitae urna at, placerat molestie est. In hac habitasse platea dictumst. Donec in dapibus eros."
            },
            new()
            {
                Title = "Семейная психология",
                Image = diesel,
                Description = "Описание"
            }
        };
#else
        var events = new List<EventViewModel>
        {
            new()
            {
                Title = "Ресурсо-ориентировочная экономика.",
                Image = zhackFresko,
                Description = "Описание Ресурсо-ориентировочная экономика."
            },
            new()
            {
                Title = "Курс теоретической физики",
                Image = albertEinshtein,
                Description = "Описание Курс теоретической физики"
            },
        };
#endif
        foreach (var @event in events)
        {
            Favourites.Add(@event);
        }
        IsRefreshing = false;
    }
}
