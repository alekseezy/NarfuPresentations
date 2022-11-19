using NarfuPresentations.Presentation.MAUI.ViewModels;

namespace NarfuPresentations.Presentation.MAUI.Pages;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(UserProfileViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}
