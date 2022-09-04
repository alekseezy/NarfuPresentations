using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.FileStorage.Requests;

[UsedImplicitly]
public record FileUploadRequest
{
    [UsedImplicitly] public string Name { get; set; } = default!;
    [UsedImplicitly] public string Extension { get; set; } = default!;
    [UsedImplicitly] public string Data { get; set; } = default!;
}
