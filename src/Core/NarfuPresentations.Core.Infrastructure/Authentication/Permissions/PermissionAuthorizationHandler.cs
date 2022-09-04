using Microsoft.AspNetCore.Authorization;

using NarfuPresentations.Core.Application.Identity.Users;
using NarfuPresentations.Core.Infrastructure.Authentication.Extensions;

namespace NarfuPresentations.Core.Infrastructure.Authentication.Permissions;

internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IUserService _userService;

    public PermissionAuthorizationHandler(IUserService userService)
    {
        _userService = userService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User.GetUserId() is { } userId &&
            await _userService.HasPermissionAsync(userId, requirement.Permission))
            context.Succeed(requirement);
    }
}
