using NarfuPresentations.Shared.Contracts.Common;

namespace NarfuPresentations.Shared.Contracts.Identity.Users.Responses;

public record UserDetailsResponse : IResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public bool IsEmailConfirmed { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
}
