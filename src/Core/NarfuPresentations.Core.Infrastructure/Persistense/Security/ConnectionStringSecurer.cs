using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

using NarfuPresentations.Core.Application.Persistense.Security;
using NarfuPresentations.Core.Infrastructure.Persistense.Settings;

namespace NarfuPresentations.Core.Infrastructure.Persistense.Security;

public class ConnectionStringSecurer : IConnectionStringSecurer
{
    private const string HiddenValueDefault = "Hidden";
    private readonly DatabaseSettings _databaseSettings;

    public ConnectionStringSecurer(IOptions<DatabaseSettings> databaseSettings) =>
        _databaseSettings = databaseSettings.Value;

    public string? Secure(string? connectionString, string? dbProvider = null) =>
        string.IsNullOrEmpty(connectionString)
            ? connectionString
            : MakeSecureSqlConnectionString(connectionString);

    private string MakeSecureSqlConnectionString(string connectionString)
    {
        var builder = new SqlConnectionStringBuilder(connectionString);

        if (!string.IsNullOrEmpty(builder.Password) || !builder.IntegratedSecurity)
        {
            builder.Password = HiddenValueDefault;
        }

        if (!string.IsNullOrEmpty(builder.UserID) || !builder.IntegratedSecurity)
        {
            builder.UserID = HiddenValueDefault;
        }

        return builder.ToString();
    }
}
