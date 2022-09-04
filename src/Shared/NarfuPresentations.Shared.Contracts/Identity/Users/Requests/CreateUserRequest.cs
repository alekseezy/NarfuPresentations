using JetBrains.Annotations;

namespace NarfuPresentations.Shared.Contracts.Identity.Users.Requests;

[UsedImplicitly]
public record CreateUserRequest
{
    [UsedImplicitly] public string FirstName { get; set; } = default!;
    [UsedImplicitly] public string LastName { get; set; } = default!;
    [UsedImplicitly] public string Email { get; set; } = default!;
    [UsedImplicitly] public string UserName { get; set; } = default!;
    [UsedImplicitly] public string Password { get; set; } = default!;
    [UsedImplicitly] public string ConfirmPassword { get; set; } = default!;
    [UsedImplicitly] public string? PhoneNumber { get; set; }
}
