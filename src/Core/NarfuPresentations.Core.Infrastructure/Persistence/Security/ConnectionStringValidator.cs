using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NarfuPresentations.Core.Application.Persistence.Security;
using NarfuPresentations.Core.Infrastructure.Persistence.Settings;

namespace NarfuPresentations.Core.Infrastructure.Persistence.Security;

internal class ConnectionStringValidator : IConnectionStringValidator
{
    private readonly DatabaseSettings _databaseSettings;
    private readonly ILogger<ConnectionStringValidator> _logger;

    public ConnectionStringValidator(IOptions<DatabaseSettings> databaseSettings,
        ILogger<ConnectionStringValidator> logger)
    {
        _databaseSettings = databaseSettings.Value;
        _logger = logger;
    }

    public bool TryValidate(string connectionString, string? dbProvider = null)
    {
        try
        {
            _ = new SqlConnectionStringBuilder(connectionString);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Connection String Validation Exception: {Message}", ex.Message);
            return false;
        }
    }
}
