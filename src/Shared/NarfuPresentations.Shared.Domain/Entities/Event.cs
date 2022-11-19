namespace NarfuPresentations.Shared.Domain.Entities;

public record Event : BaseAuditableEntity<Guid>, IAggregateRoot
{
    public string Title { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public AccessType AccessType { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid CreatorId { get; set; } = default!;

    public DateTime StartsOn { get; set; } = default!;

    public IEnumerable<Participant> Participants { get; set; } = default!;
    public IEnumerable<Presentation> Presentations { get; set; } = default!;
}
