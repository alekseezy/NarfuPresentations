// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.Common.Filters;

public static class FilterOperator
{
    public const string EQ = "eq";
    public const string NEQ = "neq";
    public const string LT = "lt";
    public const string LTE = "lte";
    public const string GT = "gt";
    public const string GTE = "gte";
    public const string STARTSWITH = "startswith";
    public const string ENDSWITH = "endswith";
    public const string CONTAINS = "contains";
}

public static class FilterLogic
{
    public const string AND = "and";
    public const string OR = "or";
    public const string XOR = "xor";
}

[UsedImplicitly]
public record Filter
{
    [UsedImplicitly] public string? Logic { get; set; }
    [UsedImplicitly] public IEnumerable<Filter>? Filters { get; set; }
    [UsedImplicitly] public string? Field { get; set; }
    [UsedImplicitly] public string? Operator { get; set; }
    [UsedImplicitly] public object? Value { get; set; }
}
