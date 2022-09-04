using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.Authentication.Constants;

[UsedImplicitly]
public static class Action
{
    [UsedImplicitly] public const string View = nameof(View);
    [UsedImplicitly] public const string Search = nameof(Search);
    [UsedImplicitly] public const string Validate = nameof(Validate);
    [UsedImplicitly] public const string Create = nameof(Create);
    [UsedImplicitly] public const string Update = nameof(Update);
    [UsedImplicitly] public const string Delete = nameof(Delete);
    [UsedImplicitly] public const string Export = nameof(Export);
    [UsedImplicitly] public const string Import = nameof(Import);
    [UsedImplicitly] public const string Generate = nameof(Generate);
    [UsedImplicitly] public const string Clean = nameof(Clean);
}

[UsedImplicitly]
public static class Resource
{
    [UsedImplicitly] public const string Users = nameof(Users);
    [UsedImplicitly] public const string UserRoles = nameof(UserRoles);
    [UsedImplicitly] public const string Roles = nameof(Roles);
    [UsedImplicitly] public const string RoleClaims = nameof(RoleClaims);
    [UsedImplicitly] public const string Announcement = nameof(Announcement);
}

[UsedImplicitly]
public record Permission(string Description, string Action, string Resource, bool IsBasic = false,
    bool IsRoot = false)
{
    [UsedImplicitly] public string Name => NameFor(Action, Resource);

    [UsedImplicitly]
    public static string NameFor(string action, string resource) =>
        $"Permissions.{resource}.{action}";
}
