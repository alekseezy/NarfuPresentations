using NarfuPresentations.Shared.Contracts.Identity.Tokens.Requests;
using NarfuPresentations.Shared.Contracts.Identity.Tokens.Responses;
using NarfuPresentations.Shared.Contracts.Identity.Users.Requests;

using Refit;

namespace NarfuPresentations.Presentation.MAUI.API.ServerAPI.Identity;

public interface IUsersApi
{
    [Post("/users/register")]
    Task<string> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken);

    [Post("/users/refresh")]
    Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request);

    [Post("/users/revoke")]
    Task RevokeTokenAsync(RevokeTokenRequest request);
}
