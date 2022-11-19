namespace NarfuPresentations.Shared.Contracts.Event.Requests;

public record DeleteEventRequest
{
    public Guid Id { get; set; }
}
