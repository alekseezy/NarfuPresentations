using System.ComponentModel.DataAnnotations;

namespace NarfuPresentations.Core.Infrastructure.Persistense.Settings;

public class DatabaseSettings : IValidatableObject
{
    public string DatabaseName { get; set; } = string.Empty;
    public bool UseInMemory { get; set; } = false;
    public string ConnectionString
    {
        get => _connectionString;
        set => _connectionString = value.Replace(DatabaseNamePattern, DatabaseName);
    }

    private string _connectionString = string.Empty;

    private const string DatabaseNamePattern = "{databaseName}";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            yield return new ValidationResult(
                $"{nameof(DatabaseSettings)}.{nameof(ConnectionString)} is not configured",
                new[] { nameof(ConnectionString) });
        }

        if (string.IsNullOrEmpty(DatabaseName))
        {
            yield return new ValidationResult(
                $"{nameof(DatabaseSettings)}.{nameof(DatabaseName)} is not configured",
                new[] { nameof(DatabaseName) });
        }
    }
}