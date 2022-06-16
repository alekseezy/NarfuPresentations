namespace NarfuPresentations.Shared.Domain.Common.Contracts;

public interface IEntity<TId>
{
    TId Id { get; }
}
