using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.Common.Filters;

[UsedImplicitly]
public record BaseFilter
{
    [UsedImplicitly] public Search? AdvancedSearch { get; set; }
    [UsedImplicitly] public string? Keyword { get; set; }
    [UsedImplicitly] public Filter? AdvancedFilter { get; set; }
}
