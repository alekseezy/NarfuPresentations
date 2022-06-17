namespace NarfuPresentations.Core.Infrastructure.Persistense.Initialization;

internal interface IDatabaseInitializer
{
    Task InitializeDatabaseAsync(CancellationToken cancellationToken);
}
