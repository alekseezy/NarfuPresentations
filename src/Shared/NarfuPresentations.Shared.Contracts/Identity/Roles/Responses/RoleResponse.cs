using JetBrains.Annotations;

using NarfuPresentations.Shared.Contracts.Common;

namespace NarfuPresentations.Shared.Contracts.Identity.Roles.Responses;

[UsedImplicitly]
public record RoleResponse : IResponse
{
    [UsedImplicitly] public string Id { get; set; } = default!;
    [UsedImplicitly] public string Name { get; set; } = default!;
    [UsedImplicitly] public string? Description { get; set; }
    [UsedImplicitly] public IEnumerable<string>? Permissions { get; set; }
}
