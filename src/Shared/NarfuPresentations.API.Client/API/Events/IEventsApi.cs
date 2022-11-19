using Refit;

namespace NarfuPresentations.Shared.API.Client.API.Events;

public interface IEventsApi
{
    [Get("/not-found")]
    Task<string> Stub(string stub);
}
