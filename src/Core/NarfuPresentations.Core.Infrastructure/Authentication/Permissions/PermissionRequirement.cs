using Microsoft.AspNetCore.Authorization;

namespace NarfuPresentations.Core.Infrastructure.Authentication.Permissions;

internal class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permission) => Permission = permission;

    public string Permission { get; }
}
