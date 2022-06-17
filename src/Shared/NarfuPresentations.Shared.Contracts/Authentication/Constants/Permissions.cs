namespace NarfuPresentations.Shared.Contracts.Authentication.Constants;

public static class Action
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Validate = nameof(Validate);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Import = nameof(Import);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
}

public static class Resource
{
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Schelters = nameof(Schelters);
    public const string Animals = nameof(Animals);
    public const string Announcement = nameof(Announcement);
}

public record Permission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}
