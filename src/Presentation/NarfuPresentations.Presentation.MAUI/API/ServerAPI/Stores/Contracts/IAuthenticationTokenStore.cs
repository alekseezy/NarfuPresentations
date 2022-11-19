namespace NarfuPresentations.Presentation.MAUI.API.ServerAPI.Stores.Contracts;

public interface IAuthenticationTokenStore
{
    string? GetToken();
    void SaveToken(string token);
    void RemoveToken();

    Task<string> GetTokenAsync();
    Task SavetokenAsync(string token);
    Task RemoveTokenAsync();
}
