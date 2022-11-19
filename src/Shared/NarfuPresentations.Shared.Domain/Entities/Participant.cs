namespace NarfuPresentations.Shared.Domain.Entities;

public record Participant : BaseAuditableEntity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public UserRole Role { get; set; }
}
