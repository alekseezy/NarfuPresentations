using NarfuPresentations.Shared.Contracts.FileStorage.Requests;
using NarfuPresentations.Shared.Domain.Common.Enums;

namespace NarfuPresentations.Core.Application.Common.FileStorage;

public interface IFileStorageService : ITransientService
{
    Task<string> UploadAsync<T>(FileUploadRequest? request, FileType supportedFileType, CancellationToken cancellationToken = default)
    where T : class;

    bool Exists(string? relativePath);

    string GetAbsolutePath(string relativePath);

    bool TryRemove(string? relativePath);
}
