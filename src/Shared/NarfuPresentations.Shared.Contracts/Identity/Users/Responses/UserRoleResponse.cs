using JetBrains.Annotations;

using NarfuPresentations.Shared.Contracts.Common;

namespace NarfuPresentations.Shared.Contracts.Identity.Users.Responses;

[UsedImplicitly]
public record UserRoleResponse : IResponse
{
    [UsedImplicitly] public string RoleId { get; set; } = default!;
    [UsedImplicitly] public string RoleName { get; set; } = default!;
    [UsedImplicitly] public bool Enabled { get; set; }
}
