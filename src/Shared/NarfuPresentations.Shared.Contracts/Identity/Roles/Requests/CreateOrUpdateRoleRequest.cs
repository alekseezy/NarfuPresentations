namespace NarfuPresentations.Shared.Contracts.Identity.Roles.Requests;

public record CreateOrUpdateRoleRequest
{
    public string? Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}