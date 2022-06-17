namespace NarfuPresentations.Shared.Contracts.Identity.Roles.Requests;

public record UpdateRolePermissionsRequest
{
    public string RoleId { get; set; } = default!;
    public IEnumerable<string> Permissions { get; set; } = default!;
}
