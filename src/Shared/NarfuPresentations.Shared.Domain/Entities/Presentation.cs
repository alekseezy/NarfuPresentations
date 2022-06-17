namespace NarfuPresentations.Shared.Domain.Entities;

public record Presentation : BaseAuditableEntity<Guid>, IAggregateRoot
{
    public string Title { get; set; } = default!;
    public IEnumerable<Slide> Slides { get; set; } = default!;
}
