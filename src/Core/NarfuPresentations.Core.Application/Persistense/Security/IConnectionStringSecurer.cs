namespace NarfuPresentations.Core.Application.Persistense.Security;

public interface IConnectionStringSecurer
{
    string? Secure(string? connectionString, string? dbProvider = null);
}
