namespace NarfuPresentations.Core.Application.Persistence.Security;

public interface IConnectionStringValidator
{
    bool TryValidate(string connectionString, string? dbProvider = null);
}
