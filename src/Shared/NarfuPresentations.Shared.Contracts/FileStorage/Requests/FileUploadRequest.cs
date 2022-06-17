namespace NarfuPresentations.Shared.Contracts.FileStorage.Requests;

public record FileUploadRequest
{
    public string Name { get; set; } = default!;
    public string Extension { get; set; } = default!;
    public string Data { get; set; } = default!;
}
