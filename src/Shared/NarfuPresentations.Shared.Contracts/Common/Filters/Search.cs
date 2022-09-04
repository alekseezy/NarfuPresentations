using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.Common.Filters;

[UsedImplicitly]
public record Search
{
    [UsedImplicitly] public List<string> Fields { get; set; } = new();
    [UsedImplicitly] public string? Keyword { get; set; }
}
