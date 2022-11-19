using System.ComponentModel;

using NarfuPresentations.Presentation.MAUI.ViewModels;

namespace NarfuPresentations.Presentation.MAUI.Pages;

[QueryProperty(nameof(Dialog), "Dialog")]
public partial class DialogPage : ContentPage
{
    private DialogViewModel dialog;

    public DialogViewModel Dialog
    {
        get => dialog;
        set
        {
            dialog = value;
            OnPropertyChanged(nameof(Dialog));
        }
    }


    public DialogPage()
    {
        InitializeComponent();
        BindingContext = this;
    }
}
