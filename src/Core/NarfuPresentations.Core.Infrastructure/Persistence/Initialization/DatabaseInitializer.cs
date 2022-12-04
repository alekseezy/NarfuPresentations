using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NarfuPresentations.Core.Infrastructure.Persistence.Initialization;

internal class DatabaseInitializer : IDatabaseInitializer
{
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider,
        ILogger<DatabaseInitializer> logger) =>
        (_serviceProvider, _logger) = (serviceProvider, logger);

    public async Task InitializeDatabaseAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        await scope.ServiceProvider
            .GetRequiredService<ApplicationDbInitializer>()
            .InitializeAsync(cancellationToken);
    }
}
