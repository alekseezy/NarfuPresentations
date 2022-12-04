using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;

namespace NarfuPresentations.Core.Infrastructure.Identity.Models;

public sealed class ApplicationRole : IdentityRole
{
    public ApplicationRole(string name)
        : base(name) =>
        NormalizedName = name.ToUpperInvariant();

    public ApplicationRole(string name, string? description = null)
        : base(name)
    {
        Description = description;
        NormalizedName = name.ToUpperInvariant();
    }

    [UsedImplicitly] public string? Description { get; set; }
}
