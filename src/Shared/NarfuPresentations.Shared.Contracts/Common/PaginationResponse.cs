using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.Common;

[UsedImplicitly]
public record PaginationResponse<TData>(IEnumerable<TData> Data, int TotalCount, int TotalPages,
    int PageSize)
{
    [UsedImplicitly] public int CurrentPage { get; set; }
    [UsedImplicitly] public bool HasNextData => CurrentPage < TotalPages;
    [UsedImplicitly] public bool HasPreviousData => CurrentPage > 1;
}
