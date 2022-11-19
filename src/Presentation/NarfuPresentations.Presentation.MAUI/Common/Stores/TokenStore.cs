using NarfuPresentations.Presentation.MAUI.API.ServerAPI.Stores.Contracts;

namespace NarfuPresentations.Presentation.MAUI.Common.Stores;

internal class TokenStore : IAuthenticationTokenStore
{
    private const string AccessToken = nameof(AccessToken);

    public string? GetToken()
    {
        return Preferences.Get(AccessToken, null);
    }

    public Task<string> GetTokenAsync()
    {
        return Task.FromResult(GetToken());
    }

    public void RemoveToken()
    {
        Preferences.Remove(AccessToken);
    }

    public async Task RemoveTokenAsync()
    {
        RemoveToken();
        
        await Task.CompletedTask;
    }

    public void SaveToken(string token)
    {
        Preferences.Set(AccessToken, token);
    }

    public async Task SavetokenAsync(string token)
    {
        SaveToken(token);
        await Task.CompletedTask;
    }
}
