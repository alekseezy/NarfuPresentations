using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using NarfuPresentations.Core.Application.Identity.Tokens;
using NarfuPresentations.Shared.Contracts.Identity.Tokens.Requests;
using NarfuPresentations.Shared.Contracts.Identity.Tokens.Responses;

using NSwag.Annotations;

namespace NarfuPresentations.Presentation.API.Controllers.Identity;

public sealed class TokensController : VersionNeutralApiController
{
    private readonly ITokenService _tokenService;

    public TokensController(ITokenService tokenService) =>
        _tokenService = tokenService;

    [HttpPost("token")]
    [AllowAnonymous]
    [OpenApiOperation("Request an access token using credentials.", "")]
    public async Task<TokenResponse> GetTokenAsync(TokenRequest request, CancellationToken cancellationToken) =>
        await _tokenService.GetTokenAsync(request, GetIpAddress(), cancellationToken);

    [HttpPost("refresh")]
    [AllowAnonymous]
    [OpenApiOperation("Request an access token using an email.", "")]
    public async Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request) =>
        await _tokenService.RefreshTokenAsync(request, GetIpAddress());

    [HttpPost("revoke")]
    [AllowAnonymous]
    [OpenApiOperation("Revokes an access token using an email.", "")]
    public async Task RevokeTokenAsync(RevokeTokenRequest request) =>
        await _tokenService.RevokeTokenAsync(request, GetIpAddress());

    private string GetIpAddress() =>
        Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"]
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";
}
