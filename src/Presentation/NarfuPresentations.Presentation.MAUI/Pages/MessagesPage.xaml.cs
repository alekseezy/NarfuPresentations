using NarfuPresentations.Presentation.MAUI.ViewModels;

namespace NarfuPresentations.Presentation.MAUI.Pages;

public partial class MessagesPage : ContentPage
{
	public MessagesPage(MessagesViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}
