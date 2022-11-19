using NarfuPresentations.Presentation.MAUI.ViewModels;

namespace NarfuPresentations.Presentation.MAUI.Pages;

public partial class FavouritesPage : ContentPage
{
	public FavouritesPage(FavouritesViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}
