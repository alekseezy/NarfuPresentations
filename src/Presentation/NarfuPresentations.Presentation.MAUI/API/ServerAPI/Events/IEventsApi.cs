using Refit;

namespace NarfuPresentations.Presentation.MAUI.API.ServerAPI.Events;

public interface IEventsApi
{
    [Get("/not-found")]
    Task<string> Stub(string stub);
}
