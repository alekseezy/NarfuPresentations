using JetBrains.Annotations;

using NarfuPresentations.Shared.Contracts.Common;

namespace NarfuPresentations.Shared.Contracts.Identity.Users.Responses;

[UsedImplicitly]
public record UserDetailsResponse : IResponse
{
    [UsedImplicitly] public Guid Id { get; set; }
    [UsedImplicitly] public string UserName { get; set; } = default!;
    [UsedImplicitly] public string FirstName { get; set; } = default!;
    [UsedImplicitly] public string LastName { get; set; } = default!;
    [UsedImplicitly] public string Email { get; set; } = default!;
    [UsedImplicitly] public bool IsActive { get; set; } = true;
    [UsedImplicitly] public bool IsEmailConfirmed { get; set; }
    [UsedImplicitly] public string PhoneNumber { get; set; } = default!;
    [UsedImplicitly] public string ImageUrl { get; set; } = default!;
}
