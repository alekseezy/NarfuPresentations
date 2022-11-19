using NarfuPresentations.Presentation.MAUI.ViewModels;

namespace NarfuPresentations.Presentation.MAUI.Pages;

[QueryProperty(nameof(Settings), nameof(SettingsViewModel))]
public partial class AppearanceSettingsPage : ContentPage
{
    private SettingsViewModel viewModel;

    public SettingsViewModel Settings
    {
        get => viewModel;
        set
        {
            viewModel = value;
            OnPropertyChanged();
        }
    }

    public AppearanceSettingsPage()
    {
        InitializeComponent();
        BindingContext = this;
    }
}
