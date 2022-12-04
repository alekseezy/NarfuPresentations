using Microsoft.AspNetCore.Mvc;

using NarfuPresentations.Core.Application.Identity.Users;
using NarfuPresentations.Core.Infrastructure.Authentication.Extensions;
using NarfuPresentations.Shared.Contracts.Identity.Users.Requests;
using NarfuPresentations.Shared.Contracts.Identity.Users.Responses;

using NSwag.Annotations;

namespace NarfuPresentations.Presentation.API.Controllers.Personal;

public sealed class PersonalController : VersionNeutralApiController
{
    private readonly IUserService _userService;

    public PersonalController(IUserService userService) => _userService = userService;

    [HttpGet("profile")]
    [OpenApiOperation("Get profile details of currently logged in user.", "")]
    public async Task<ActionResult<UserDetailsResponse>> GetProfileAsync(
        CancellationToken cancellationToken) =>
        User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
            ? Unauthorized()
            : Ok(await _userService.GetAsync(userId, cancellationToken));

    [HttpPut("profile")]
    [OpenApiOperation("Update profile details of currently logged in user.", "")]
    public async Task<ActionResult> UpdateProfileAsync(UpdateUserRequest request)
    {
        if (User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
            return Unauthorized();

        await _userService.UpdateAsync(request, userId);
        return Ok();
    }

    [HttpGet("permissions")]
    [OpenApiOperation("Get permissions of currently logged in user.", "")]
    public async Task<ActionResult<IEnumerable<string>>> GetPermissionsAsync(
        CancellationToken cancellationToken) =>
        User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
            ? Unauthorized()
            : Ok(await _userService.GetPermissionsAsync(userId, cancellationToken));
}
