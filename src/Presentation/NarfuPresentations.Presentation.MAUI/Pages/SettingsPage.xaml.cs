using NarfuPresentations.Presentation.MAUI.ViewModels;

namespace NarfuPresentations.Presentation.MAUI.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}
