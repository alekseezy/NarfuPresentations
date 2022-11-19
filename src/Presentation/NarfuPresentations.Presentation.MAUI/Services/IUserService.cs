using NarfuPresentations.Presentation.MAUI.Models;

namespace NarfuPresentations.Presentation.MAUI.Services;

public interface IUserService
{
    bool IsLoggedIn { get; }
    string UserName { get; }
    string Email { get; }
    string PhoneNumber { get; }
    string FirstName { get; }
    string LastName { get; }

    public Task<ApplicatonUser> GetUserAsync();

    public Task RegisterAsync(ApplicatonUser user, CancellationToken cancellationToken = default);
    public Task LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    public Task LogoutAsync(CancellationToken cancellationToken = default);
}
