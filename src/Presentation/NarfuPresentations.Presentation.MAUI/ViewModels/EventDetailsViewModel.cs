using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NarfuPresentations.Presentation.MAUI.Models;
using NarfuPresentations.Presentation.MAUI.Services;
using NarfuPresentations.Shared.Domain.Entities;

namespace NarfuPresentations.Presentation.MAUI.ViewModels;

[QueryProperty(nameof(Event), "Event")]
public sealed partial class EventDetailsViewModel : BaseViewModel
{
    private readonly IUserService userService;
    [ObservableProperty]
    private EventViewModel _event;

    public ObservableCollection<ApplicatonUser> Participants { get; }

    public EventDetailsViewModel(IUserService userService)
    {
        Participants = new();
        this.userService = userService;
        RefreshParticipantsAsync();
    }

    [RelayCommand]
    private async Task AddMeAsync()
    {
        Participants.Add(new ApplicatonUser() { FirstName = userService.FirstName, LastName = userService.LastName });
    }

    [RelayCommand]
    private async Task RefreshParticipantsAsync()
    {
        var partisipants = new List<ApplicatonUser>
        {
            new()
            {
                FirstName = "1 Иван",
                LastName = "Синичкин",
            },
            new()
            {
                FirstName = "2 Иван",
                LastName = "Синичкин",
            },
            new()
            {
                FirstName = "3 Иван",
                LastName = "Синичкин",
            },
            new()
            {
                FirstName = "4 Иван",
                LastName = "Синичкин",
            },
            new()
            {
                FirstName = "5 Иван",
                LastName = "Синичкин",
            },
            new()
            {
                FirstName = "6 Иван",
                LastName = "Синичкин",
            },
            new()
            {
                FirstName = "7 Иван",
                LastName = "Синичкин",
            },
            new()
            {
                FirstName = "8 Иван",
                LastName = "Синичкин",
            }
        };

        var random = new Random();

        var count = random.Next(1, partisipants.Count);

        for (var i = 0; i < count; i++)
        {
            var index = random.Next(1, partisipants.Count);
            Participants.Add(partisipants[index]);
        }
    }
}
