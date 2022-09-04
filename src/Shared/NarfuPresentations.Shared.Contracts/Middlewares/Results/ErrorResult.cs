using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.Middlewares.Results;

public record ErrorResult
{
    public List<string> Messages { get; set; } = new();

    [UsedImplicitly] public string? Source { get; set; }
    [UsedImplicitly] public string? Exception { get; set; }
    [UsedImplicitly] public string? ErrorId { get; set; }
    [UsedImplicitly] public string? SupportMessage { get; set; }
    [UsedImplicitly] public int StatusCode { get; set; }
}
