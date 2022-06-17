using Microsoft.Extensions.DependencyInjection;

namespace NarfuPresentations.Core.Infrastructure.Persistense.Initialization;

internal class SeederRunner
{
    private readonly ISeeder[] _seeders;

    public SeederRunner(IServiceProvider serviceProvider) =>
        _seeders = serviceProvider.GetServices<ISeeder>().ToArray();

    public async Task RunSeedersAsync(CancellationToken cancellationToken)
    {
        foreach (var seeder in _seeders)
        {
            await seeder.RunAsync(cancellationToken);
        }
    }
}
