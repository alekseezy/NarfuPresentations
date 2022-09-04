using NarfuPresentations.Shared.Contracts.Common;

namespace NarfuPresentations.Shared.Contracts.Identity.Tokens.Responses;

public record TokenResponse
    (string Token, string RefreshToken, DateTime RefreshTokenExpireTime) : IResponse;
