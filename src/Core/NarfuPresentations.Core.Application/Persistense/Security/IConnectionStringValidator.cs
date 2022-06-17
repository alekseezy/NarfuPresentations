namespace NarfuPresentations.Core.Application.Persistense.Security;

public interface IConnectionStringValidator
{
    bool TryValidate(string connectionString, string? dbProvider = null);
}
