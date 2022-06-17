namespace NarfuPresentations.Shared.Contracts.Common;

public record PaginationResponse<TData>(IEnumerable<TData> Data, int TotalCount, int TotalPages, int PageSize)
{
    public int CurrentPage { get; set; }
    public bool HasNextData => CurrentPage < TotalPages;
    public bool HasPreviousData => CurrentPage > 1;
}