using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.Identity.Roles.Requests;

[UsedImplicitly]
public record UpdateRolePermissionsRequest
{
    [UsedImplicitly] public string RoleId { get; set; } = default!;
    [UsedImplicitly] public IEnumerable<string> Permissions { get; set; } = default!;
}
