using NarfuPresentations.Core.Application.Common;
using NarfuPresentations.Shared.Contracts.Identity.Roles.Requests;
using NarfuPresentations.Shared.Contracts.Identity.Roles.Responses;

namespace NarfuPresentations.Core.Application.Identity.Roles;

public interface IRoleService : ITransientService
{
    Task<List<RoleResponse>> GetListAsync(CancellationToken cancellationToken);

    Task<int> GetCountAsync(CancellationToken cancellationToken);

    Task<bool> ExistsAsync(string roleName, string? excludeId);

    Task<RoleResponse> GetByIdAsync(string id);

    Task<RoleResponse> GetByIdWithPermissionsAsync(string roleId, CancellationToken cancellationToken);

    Task<string> CreateOrUpdateAsync(CreateOrUpdateRoleRequest request);

    Task<string> UpdatePermissionsAsync(UpdateRolePermissionsRequest request, CancellationToken cancellationToken);

    Task<string> DeleteAsync(string id);
}
