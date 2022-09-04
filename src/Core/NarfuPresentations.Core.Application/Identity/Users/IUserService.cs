using NarfuPresentations.Core.Application.Common;
using NarfuPresentations.Shared.Contracts.Common;
using NarfuPresentations.Shared.Contracts.Identity.Users.Filters;
using NarfuPresentations.Shared.Contracts.Identity.Users.Requests;
using NarfuPresentations.Shared.Contracts.Identity.Users.Responses;

namespace NarfuPresentations.Core.Application.Identity.Users;

public interface IUserService : ITransientService
{
    Task<PaginationResponse<UserDetailsResponse>> SearchAsync(UsersFilter filter,
        CancellationToken cancellationToken);

    Task<bool> ExistsWithEmailAsync(string email);
    Task<bool> ExistsWithUserNameAsync(string userName);
    Task<bool> ExistsWithPhoneNumberAsync(string phone);

    Task<string> CreateAsync(CreateUserRequest request, string origin,
        CancellationToken cancellationToken);

    Task UpdateAsync(UpdateUserRequest request, string userId);

    Task<string> ConfirmEmailAsync(string userId, string code);

    Task<UserDetailsResponse> GetAsync(string userId, CancellationToken cancellationToken);
    Task<IEnumerable<UserDetailsResponse>> GetAllAsync(CancellationToken cancellationToken);

    Task<IEnumerable<UserRoleResponse>> GetRolesAsync(string userId,
        CancellationToken cancellationToken);

    Task<string> AssignRolesAsync(string userId, AssignUserRolesRequest request,
        CancellationToken cancellationToken);

    Task<IEnumerable<string>> GetPermissionsAsync(string userId,
        CancellationToken cancellationToken);

    Task<bool> HasPermissionAsync(string userId, string permission,
        CancellationToken cancellationToken = default);
}
