using JetBrains.Annotations;

using NarfuPresentations.Shared.Contracts.FileStorage.Requests;

namespace NarfuPresentations.Shared.Contracts.Identity.Users.Requests;

[UsedImplicitly]
public record UpdateUserRequest
{
    [UsedImplicitly] public string Id { get; set; } = default!;
    [UsedImplicitly] public string? FirstName { get; set; }
    [UsedImplicitly] public string? LastName { get; set; }
    [UsedImplicitly] public string? PhoneNumber { get; set; }
    [UsedImplicitly] public string? Email { get; set; }
    [UsedImplicitly] public FileUploadRequest? Image { get; set; }
    [UsedImplicitly] public bool DeleteCurrentImage { get; set; }
}
