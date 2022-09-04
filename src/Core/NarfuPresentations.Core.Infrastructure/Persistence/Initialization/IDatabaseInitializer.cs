namespace NarfuPresentations.Core.Infrastructure.Persistence.Initialization;

internal interface IDatabaseInitializer
{
    Task InitializeDatabaseAsync(CancellationToken cancellationToken);
}
