using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using NarfuPresentations.Core.Application.Authentication.Exceptions;
using NarfuPresentations.Core.Application.Identity.Tokens;
using NarfuPresentations.Core.Infrastructure.Authentication.Extensions;
using NarfuPresentations.Core.Infrastructure.Authentication.JWT.Settings;
using NarfuPresentations.Core.Infrastructure.Identity.Models;
using NarfuPresentations.Shared.Contracts.Authentication.Constants;
using NarfuPresentations.Shared.Contracts.Identity.Tokens.Requests;
using NarfuPresentations.Shared.Contracts.Identity.Tokens.Responses;

namespace NarfuPresentations.Core.Infrastructure.Identity.Services;

// TODO:
// TokenService should securely handle access tokens and refresh tokens
// but for now this is not possible and not on my first priority
//
public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<ApplicationUser> _userManager;

    public TokenService(UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings) =>
        (_userManager, _jwtSettings) = (userManager, jwtSettings.Value);

    public async Task<TokenResponse> GetTokenAsync(TokenRequest request, string ipAddress,
        CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(request.Email.Trim().Normalize()) is not { } user
            || !await _userManager.CheckPasswordAsync(user, request.Password))
            throw new UnauthorizedException("Authentication Failed.");

        if (!user.IsActive)
            throw new UnauthorizedException(
                "User is not active. Please contact the administrator.");

        return await GenerateTokensAndUpdateUser(user, ipAddress);
    }

    public async Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request,
        string ipAddress)
    {
        throw new NotImplementedException();

        var userPrincipal = GetPrincipalFromExpiredToken(request.Token);
        var userEmail = userPrincipal.GetEmail();
        var user = await _userManager.FindByEmailAsync(userEmail)
                   ?? throw new UnauthorizedException("Authentication Failed.");
    }

    public async Task RevokeTokenAsync(RevokeTokenRequest request, string ipAddress) =>
        throw new NotImplementedException();

    //if (await _userManager.FindByEmailAsync(request.Email.Trim().Normalize()) is not { } user
    //    || !await _userManager.VerifyUserTokenAsync(user, "jwt", "Revoke", request.Token))
    //    throw new UnauthorizedException("Authentication Failed.");
    //user.RefreshTokenExpireTime = DateTime.Now;
    //await _userManager.UpdateAsync(user);
    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token) =>
        throw new NotImplementedException();

    private async Task<TokenResponse> GenerateTokensAndUpdateUser(ApplicationUser user,
        string ipAddress)
    {
        var token = GenerateJwt(user, ipAddress);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpireTime =
            DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenExpirationInMinutes);

        await _userManager.UpdateAsync(user);

        return new TokenResponse(token, refreshToken, user.RefreshTokenExpireTime);
    }

    private string GenerateJwt(ApplicationUser user, string ipAddress) =>
        GenerateEncryptedToken(GetSigningCredentials(), GetClaims(user, ipAddress));

    private IEnumerable<Claim> GetClaims(ApplicationUser user, string ipAddress) =>
        new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email),
            new(ClaimConstants.Fullname, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.Name, user.FirstName ?? string.Empty),
            new(ClaimTypes.Surname, user.LastName ?? string.Empty),
            new(ClaimConstants.IpAddress, ipAddress),
            new(ClaimConstants.ImageUrl, user.ImageUrl ?? string.Empty),
            new(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty)
        };

    private SigningCredentials GetSigningCredentials() =>
        new(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
            SecurityAlgorithms.HmacSha256);

    private string GenerateEncryptedToken(SigningCredentials signingCredentials,
        IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
            signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = RandomNumberGenerator.GetBytes(32);

        return Convert.ToBase64String(randomNumber);
    }
}
