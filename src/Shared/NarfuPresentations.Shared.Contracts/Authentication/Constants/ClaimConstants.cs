using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.Authentication.Constants;

[UsedImplicitly]
public static class ClaimConstants
{
    [UsedImplicitly] public const string Permission = nameof(Permission);
    [UsedImplicitly] public const string Fullname = nameof(Fullname);
    [UsedImplicitly] public const string IpAddress = nameof(IpAddress);
    [UsedImplicitly] public const string ImageUrl = nameof(ImageUrl);
    [UsedImplicitly] public const string Expiration = nameof(Expiration);
}
