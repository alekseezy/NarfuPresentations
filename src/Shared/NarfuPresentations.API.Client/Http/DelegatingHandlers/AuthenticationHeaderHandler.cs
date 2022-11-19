using System.Net.Http.Headers;

using NarfuPresentations.Shared.API.Client.API.Stores.Contracts;

namespace NarfuPresentations.Shared.API.Client.Http.DelegatingHandlers;

internal class AuthenticationHeaderHandler : DelegatingHandler
{
    private readonly IAuthenticationTokenStore _authenticationTokenStore;

    public AuthenticationHeaderHandler(IAuthenticationTokenStore authenticationTokenStore)
    {
        ArgumentNullException.ThrowIfNull(authenticationTokenStore);

        _authenticationTokenStore = authenticationTokenStore;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
    {
        var token = await _authenticationTokenStore.GetTokenAsync();

        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(requestMessage, cancellationToken);
    }
}
