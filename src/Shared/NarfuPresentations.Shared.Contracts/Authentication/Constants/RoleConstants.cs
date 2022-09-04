using System.Collections.ObjectModel;

using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.Authentication.Constants;

[UsedImplicitly]
public static class RoleConstants
{
    [UsedImplicitly] public const string Admin = nameof(Admin);
    [UsedImplicitly] public const string Basic = nameof(Basic);

    [UsedImplicitly]
    public static IEnumerable<string> DefaultRoles =>
        new ReadOnlyCollection<string>(
            new[]
            {
                Admin,
                Basic
            });

    [UsedImplicitly]
    public static bool IsDefault(string roleName) => DefaultRoles.Any(role => role == roleName);
}
