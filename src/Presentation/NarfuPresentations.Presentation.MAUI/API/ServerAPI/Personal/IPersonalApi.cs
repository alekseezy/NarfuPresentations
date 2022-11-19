using NarfuPresentations.Shared.Contracts.Identity.Users.Requests;
using NarfuPresentations.Shared.Contracts.Identity.Users.Responses;

using Refit;

namespace NarfuPresentations.Presentation.MAUI.API.ServerAPI.Personal;

public interface IPersonalApi
{
    [Get("/personal/profile")]
    [Headers("Authorization: Bearer")]
    Task<IApiResponse<UserDetailsResponse>> GetProfileAsync(CancellationToken cancellationToken);

    [Put("/personal/profile")]
    [Headers("Authorization: Bearer")]
    Task<IApiResponse> UpdateProfileAsync(UpdateUserRequest request);

    [Get("/personal/permissions")]
    [Headers("Authorization: Bearer")]
    Task<IApiResponse<IEnumerable<string>>> GetPermissionsAsync(CancellationToken cancellationToken);
}
