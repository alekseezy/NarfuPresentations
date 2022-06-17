using Microsoft.AspNetCore.Identity;

namespace NarfuPresentations.Core.Infrastructure.Identity.Models;

public class ApplicationRole : IdentityRole
{
    public string? Description { get; set; }

    public ApplicationRole(string name)
        : base(name)
    {
        NormalizedName = name.ToUpperInvariant();
    }

    public ApplicationRole(string name, string? description = null)
        : base(name)
    {
        Description = description;
        NormalizedName = name.ToUpperInvariant();
    }
}
