using NarfuPresentations.Presentation.MAUI.ViewModels;

namespace NarfuPresentations.Presentation.MAUI.Pages;

public partial class EventDetailsPage : ContentPage
{
	public EventDetailsPage(EventDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}
