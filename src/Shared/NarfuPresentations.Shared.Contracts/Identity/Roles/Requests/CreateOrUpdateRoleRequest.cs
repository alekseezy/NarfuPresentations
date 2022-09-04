using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.Identity.Roles.Requests;

[UsedImplicitly]
public record CreateOrUpdateRoleRequest
{
    [UsedImplicitly] public string? Id { get; set; }
    [UsedImplicitly] public string Name { get; set; } = default!;
    [UsedImplicitly] public string? Description { get; set; }
}
