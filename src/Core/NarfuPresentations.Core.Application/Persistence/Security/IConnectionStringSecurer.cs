namespace NarfuPresentations.Core.Application.Persistence.Security;

public interface IConnectionStringSecurer
{
    string? Secure(string? connectionString, string? dbProvider = null);
}
