using NarfuPresentations.Presentation.MAUI.Pages;
using NarfuPresentations.Presentation.MAUI.ViewModels;

namespace NarfuPresentations.Presentation.MAUI.Handlers;

public class EventSearchHandler : SearchHandler
{
    public IList<EventViewModel> Events { get; set; }
    public Type SelectedItemNavigationTarget { get; set; }

    private static bool isInilialized;

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if (!isInilialized)
        {
            Events = ItemsSource as IList<EventViewModel>;
            isInilialized = true;
        }

        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = null;
        }
        else
        {
           ItemsSource = Events
                .Where(@event => @event?.Title?.ToLower()?.Contains(newValue?.ToLower()) is true)
                .ToList();
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        // Let the animation complete
        await Task.Delay(300);

        ShellNavigationState state = (Application.Current.MainPage as Shell).CurrentState;
        // The following route works because route names are unique in this app.
        await Shell.Current.GoToAsync(nameof(EventDetailsPage), true, new Dictionary<string, object>
        {
            { "Event", item }
        });
    }
}
