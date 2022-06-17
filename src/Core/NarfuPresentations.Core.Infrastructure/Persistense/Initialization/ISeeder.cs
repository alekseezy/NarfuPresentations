namespace NarfuPresentations.Core.Infrastructure.Persistense.Initialization;

public interface ISeeder
{
    Task RunAsync(CancellationToken cancellationToken);
}
