namespace NarfuPresentations.Core.Infrastructure.Persistence.Initialization;

public interface ISeeder
{
    Task RunAsync(CancellationToken cancellationToken);
}
