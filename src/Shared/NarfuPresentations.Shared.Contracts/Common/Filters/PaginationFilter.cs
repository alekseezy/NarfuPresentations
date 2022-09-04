using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.Common.Filters;

[UsedImplicitly]
public record PaginationFilter : BaseFilter
{
    [UsedImplicitly] public int PageNumber { get; set; }
    [UsedImplicitly] public int PageSize { get; set; } = int.MaxValue;
    [UsedImplicitly] public string[]? OrderBy { get; set; }
}

[UsedImplicitly]
public static class PaginationFilterExtensions
{
    [UsedImplicitly]
    public static bool HasOrderBy(this PaginationFilter filter) => filter.OrderBy?.Any() is true;
}
