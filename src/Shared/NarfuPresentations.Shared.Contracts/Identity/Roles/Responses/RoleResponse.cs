using NarfuPresentations.Shared.Contracts.Common;

namespace NarfuPresentations.Shared.Contracts.Identity.Roles.Responses;

public record RoleResponse : IResponse
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public IEnumerable<string>? Permissions { get; set; }
}
