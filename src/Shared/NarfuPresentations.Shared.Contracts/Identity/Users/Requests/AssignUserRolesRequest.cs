using JetBrains.Annotations;

using NarfuPresentations.Shared.Contracts.Identity.Users.Responses;

namespace NarfuPresentations.Shared.Contracts.Identity.Users.Requests;

[UsedImplicitly]
public record AssignUserRolesRequest
{
    [UsedImplicitly] public IEnumerable<UserRoleResponse> UserRoles { get; set; } = default!;
}
