using System.Collections.ObjectModel;

using NarfuPresentations.Shared.Contracts.Authentication.Constants;

using Action = NarfuPresentations.Shared.Contracts.Authentication.Constants.Action;

namespace NarfuPresentations.Core.Infrastructure.Authentication.Constants;

internal static class Permissions
{
    // ReSharper disable once InconsistentNaming
    private static readonly Permission[] _all =
    {
        new("View Users", Action.View, Resource.Users, true),
        new("Search Users", Action.Search, Resource.Users),
        new("Create Users", Action.Create, Resource.Users),
        new("Update Users", Action.Update, Resource.Users),
        new("Delete Users", Action.Delete, Resource.Users),
        new("Export Users", Action.Export, Resource.Users),

        new("View UserRoles", Action.View, Resource.UserRoles),
        new("Update UserRoles", Action.Update, Resource.UserRoles),

        new("View Roles", Action.View, Resource.Roles),
        new("Create Roles", Action.Create, Resource.Roles),
        new("Update Roles", Action.Update, Resource.Roles),
        new("Delete Roles", Action.Delete, Resource.Roles),

        new("View RoleClaims", Action.View, Resource.RoleClaims),
        new("Update RoleClaims", Action.Update, Resource.RoleClaims),

        new("View Announcement", Action.View, Resource.Announcement, true),
        new("Create Announcement", Action.Create, Resource.Announcement, true),
        new("Update Announcement", Action.Update, Resource.Announcement, true),
        new("Delete Announcement", Action.Delete, Resource.Announcement, true)
    };

    public static IReadOnlyList<Permission> All => new ReadOnlyCollection<Permission>(_all);

    public static IReadOnlyList<Permission> Root =>
        new ReadOnlyCollection<Permission>(_all.Where(p => p.IsRoot).ToArray());

    public static IReadOnlyList<Permission> Admin =>
        new ReadOnlyCollection<Permission>(_all.Where(p => !p.IsRoot).ToArray());

    public static IReadOnlyList<Permission> Basic =>
        new ReadOnlyCollection<Permission>(_all.Where(p => p.IsBasic).ToArray());
}
