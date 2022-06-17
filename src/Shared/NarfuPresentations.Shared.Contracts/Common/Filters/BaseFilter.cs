namespace NarfuPresentations.Shared.Contracts.Common.Filters;

public record BaseFilter
{
    public Search? AdvancedSearch { get; set; }
    public string? Keyword { get; set; }
    public Filter? AdvancedFilter { get; set; }
}
