using NarfuPresentations.Shared.Contracts.Identity.Users.Responses;

namespace NarfuPresentations.Shared.Contracts.Identity.Users.Requests;

public record AssignUserRolesRequest
{
    public IEnumerable<UserRoleResponse> UserRoles { get; set; } = default!;
}
