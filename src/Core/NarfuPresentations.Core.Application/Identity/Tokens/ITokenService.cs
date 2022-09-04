using NarfuPresentations.Core.Application.Common;
using NarfuPresentations.Shared.Contracts.Identity.Tokens.Requests;
using NarfuPresentations.Shared.Contracts.Identity.Tokens.Responses;

namespace NarfuPresentations.Core.Application.Identity.Tokens;

public interface ITokenService : ITransientService
{
    Task<TokenResponse> GetTokenAsync(TokenRequest request, string ipAddress,
        CancellationToken cancellationToken);

    Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
    Task RevokeTokenAsync(RevokeTokenRequest request, string ipAddress);
}
