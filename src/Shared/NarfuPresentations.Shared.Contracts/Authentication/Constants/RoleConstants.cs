using System.Collections.ObjectModel;

namespace NarfuPresentations.Shared.Contracts.Authentication.Constants;

public static class RoleConstants
{
    public const string Admin = nameof(Admin);
    public const string Basic = nameof(Basic);

    public static IReadOnlyList<string> DefaultRoles =>
        new ReadOnlyCollection<string>(
            new[] {
            Admin,
            Basic
            });

    public static bool IsDefault(string roleName) =>
        DefaultRoles.Any(role => role == roleName);
}
