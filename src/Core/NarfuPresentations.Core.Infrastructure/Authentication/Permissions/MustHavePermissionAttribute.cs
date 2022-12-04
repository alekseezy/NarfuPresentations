using Microsoft.AspNetCore.Authorization;

using NarfuPresentations.Shared.Contracts.Authentication.Constants;

namespace NarfuPresentations.Core.Infrastructure.Authentication.Permissions;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = Permission.NameFor(action, resource);
}
