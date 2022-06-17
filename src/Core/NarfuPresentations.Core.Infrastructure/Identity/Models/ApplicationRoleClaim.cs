using Microsoft.AspNetCore.Identity;

namespace NarfuPresentations.Core.Infrastructure.Identity.Models;

public class ApplicationRoleClaim : IdentityRoleClaim<string>
{
    public string? Description { get; set; }
    public string? CreatedBy { get; set; }
}
