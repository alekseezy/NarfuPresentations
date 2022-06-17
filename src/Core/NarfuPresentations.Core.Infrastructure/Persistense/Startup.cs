using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using NarfuPresentations.Core.Application.Common.DependencyInjection;
using NarfuPresentations.Core.Application.Persistense;
using NarfuPresentations.Core.Application.Persistense.Security;
using NarfuPresentations.Core.Infrastructure.Persistense.Context;
using NarfuPresentations.Core.Infrastructure.Persistense.Initialization;
using NarfuPresentations.Core.Infrastructure.Persistense.Repository;
using NarfuPresentations.Core.Infrastructure.Persistense.Security;
using NarfuPresentations.Core.Infrastructure.Persistense.Settings;
using NarfuPresentations.Shared.Domain.Common.Contracts;

using Serilog;

namespace NarfuPresentations.Core.Infrastructure.Persistense;

internal static class Startup
{
    private static readonly ILogger _logger = Log.ForContext(typeof(Startup));

    internal static IServiceCollection AddPersistense(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<DatabaseSettings>()
            .BindConfiguration(nameof(DatabaseSettings))
            .PostConfigure(databaseSettings =>
            {
                _logger.Information("Current Database Name: {name}", databaseSettings.DatabaseName);
                _logger.Information("Is In Memory Database: {useInMemory}", databaseSettings.UseInMemory);
            })
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var databaseSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;

            if (databaseSettings.UseInMemory)
                options.UseInMemoryDatabase(databaseSettings.DatabaseName);
            else
                options.UseSqlServer(databaseSettings.ConnectionString, builder => builder.MigrationsAssembly("Migrators.MSSQL"));
        });

        services
            .AddTransient<IDatabaseInitializer, DatabaseInitializer>()
            .AddTransient<ApplicationDbInitializer>()
            .AddTransient<ApplicationDbSeeder>()
            .AddServices(typeof(ISeeder), ServiceLifetime.Transient)
            .AddTransient<SeederRunner>()
            .AddTransient<IConnectionStringSecurer, ConnectionStringSecurer>()
            .AddTransient<IConnectionStringValidator, ConnectionStringValidator>()
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Add Repositories
        services.AddScoped(typeof(IRepository<>), typeof(ApplicationDbRepository<>));

        foreach (var aggregateRootType in typeof(IAggregateRoot).Assembly.GetExportedTypes()
            .Where(type => typeof(IAggregateRoot).IsAssignableFrom(type) && type.IsClass))
        {
            // Add Read Repositories
            services.AddScoped(typeof(IReadRepository<>).MakeGenericType(aggregateRootType), serviceProvider =>
                serviceProvider.GetRequiredService(typeof(IRepository<>).MakeGenericType(aggregateRootType)));
        }

        return services;
    }
}
