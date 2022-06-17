using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NarfuPresentations.Core.Application.Identity.Users;
using NarfuPresentations.Shared.Contracts.Identity.Users.Requests;

using NSwag.Annotations;

namespace NarfuPresentations.Presentation.API.Controllers.Identity;

public sealed class UsersController : VersionNeutralApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService) =>
        _userService = userService;

    [HttpPost("register")]
    [AllowAnonymous]
    [OpenApiOperation("Anonimous user creates a user.", "Creates a new account for user.")]
    public async Task<string> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken) =>
        await _userService.CreateAsync(request, GetOriginFromRequest(), cancellationToken);

    private string GetOriginFromRequest() => $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";
}
