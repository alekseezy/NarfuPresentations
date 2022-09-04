using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.Identity.Tokens.Requests;

[UsedImplicitly]
public record RevokeTokenRequest(string Token);
