using NarfuPresentations.Shared.Domain.Entities;

namespace NarfuPresentations.Shared.Contracts.Event.Requests;

public record UpdateEventRequest
{
    public string Title { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public AccessType AccessType { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid CreatorId { get; set; } = default!;

    public DateTime StartsOn { get; set; } = default!;
}
