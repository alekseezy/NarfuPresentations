using NarfuPresentations.Shared.Contracts.FileStorage.Requests;
using NarfuPresentations.Shared.Domain.Entities;

namespace NarfuPresentations.Shared.Contracts.Event.Requests;

public record CreateEventRequest
{
    public string Title { get; set; } = default!;
    public FileUploadRequest Image { get; set; } = default!;
    public AccessType AccessType { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid CreatorId { get; set; } = default!;

    public DateTime StartsOn { get; set; } = default!;

    public IEnumerable<(Guid UserId, UserRole Role)> Participants { get; set; } = default!;
    public IEnumerable<Presentation> Presentations { get; set; } = default!;
}
