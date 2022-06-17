namespace NarfuPresentations.Shared.Contracts.Common.Filters;

public record Search
{
    public List<string> Fields { get; set; } = new();
    public string? Keyword { get; set; }
}