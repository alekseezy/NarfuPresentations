using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.Identity.Tokens.Requests;

[UsedImplicitly]
public record TokenRequest(string Email, string Password);
