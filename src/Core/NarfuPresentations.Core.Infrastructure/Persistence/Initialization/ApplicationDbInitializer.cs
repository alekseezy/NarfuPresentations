using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NarfuPresentations.Core.Infrastructure.Persistence.Context;
using NarfuPresentations.Core.Infrastructure.Persistence.Settings;

namespace NarfuPresentations.Core.Infrastructure.Persistence.Initialization;

internal class ApplicationDbInitializer
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ApplicationDbSeeder _dbSeeder;
    private readonly ILogger<ApplicationDbInitializer> _logger;
    private readonly DatabaseSettings _options;

    public ApplicationDbInitializer(ApplicationDbContext dbContext, ApplicationDbSeeder dbSeeder,
        IOptions<DatabaseSettings> options, ILogger<ApplicationDbInitializer> logger)
    {
        _dbContext = dbContext;
        _dbSeeder = dbSeeder;
        _options = options.Value;
        _logger = logger;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (_options.UseInMemory)
            await InitializeInMemoryDatabaseAsync(cancellationToken);
        else
            await InitializeRelationalDatabaseAsync(cancellationToken);
    }

    private async Task InitializeInMemoryDatabaseAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Migrations wouldn't be applied because of using InMemory Database");
        if (await _dbContext.Database.CanConnectAsync(cancellationToken))
        {
            _logger.LogInformation("Connection to Database Succeeded");
            await _dbSeeder.SeedDatabaseAsync(_dbContext, cancellationToken);
        }
    }

    private async Task InitializeRelationalDatabaseAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Database.GetMigrations().Any())
        {
            if ((await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
            {
                _logger.LogInformation("Applying Migrations");
                await _dbContext.Database.MigrateAsync(cancellationToken);
            }

            if (await _dbContext.Database.CanConnectAsync(cancellationToken))
            {
                _logger.LogInformation("Connection to Database Succeeded");
                await _dbSeeder.SeedDatabaseAsync(_dbContext, cancellationToken);
            }
        }
    }
}
