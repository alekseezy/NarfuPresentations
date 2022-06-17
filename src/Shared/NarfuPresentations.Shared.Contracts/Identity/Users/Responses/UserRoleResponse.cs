using NarfuPresentations.Shared.Contracts.Common;

namespace NarfuPresentations.Shared.Contracts.Identity.Users.Responses;

public record UserRoleResponse : IResponse
{
    public string RoleId { get; set; } = default!;
    public string RoleName { get; set; } = default!;
    public bool Enabled { get; set; }
}
