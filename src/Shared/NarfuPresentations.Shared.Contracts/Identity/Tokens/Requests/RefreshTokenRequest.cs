using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.Identity.Tokens.Requests;

[UsedImplicitly]
public record RefreshTokenRequest(string Token, string RefreshToken);
