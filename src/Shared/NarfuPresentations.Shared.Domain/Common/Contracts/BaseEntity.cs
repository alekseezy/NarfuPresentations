namespace NarfuPresentations.Shared.Domain.Common.Contracts;

public abstract record BaseEntity<TId> : IEntity<TId>
    where TId : struct
{
    public TId Id { get; protected set; } = default;
}
