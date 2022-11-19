using NarfuPresentations.Shared.Contracts.Identity.Tokens.Requests;
using NarfuPresentations.Shared.Contracts.Identity.Tokens.Responses;

using Refit;

namespace NarfuPresentations.Shared.API.Client.API.Identity;

public interface ITokensApi
{
    [Post("/tokens/token")]
    Task<TokenResponse> GetTokenAsync(TokenRequest request, CancellationToken cancellationToken);
}
