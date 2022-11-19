using NarfuPresentations.Presentation.MAUI.API.ServerAPI.Identity;
using NarfuPresentations.Presentation.MAUI.API.ServerAPI.Personal;
using NarfuPresentations.Presentation.MAUI.API.ServerAPI.Stores.Contracts;
using NarfuPresentations.Presentation.MAUI.Models;
using NarfuPresentations.Shared.Contracts.Identity.Tokens.Requests;
using NarfuPresentations.Shared.Contracts.Identity.Users.Requests;

namespace NarfuPresentations.Presentation.MAUI.Services;

public class DefaultUserService : IUserService
{
    private readonly IAuthenticationTokenStore _tokenStore;
    private readonly IPersonalApi _personalApi;
    private readonly ITokensApi _tokensApi;
    private readonly IUsersApi _usersApi;

    public bool IsLoggedIn { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public DefaultUserService(IUsersApi usersApi, ITokensApi tokensApi, IPersonalApi personalApi, IAuthenticationTokenStore tokenStore)
    {
        _usersApi = usersApi;
        _tokensApi = tokensApi;
        _tokenStore = tokenStore;
        _personalApi = personalApi;

        IsLoggedIn = _tokenStore.GetToken() is not null;
    }

    public async Task RegisterAsync(ApplicatonUser user, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        var createUserRequest = new CreateUserRequest()
        {
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            ConfirmPassword = user.ConfirmPassword,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password
        };

        await _usersApi.CreateAsync(createUserRequest, default);

        try
        {
            await LoginAsync(user.Email, user.Password, cancellationToken);
        }
        catch { }
    }

    public async Task LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(email, nameof(email));
        ArgumentNullException.ThrowIfNull(password, nameof(password));

        var tokenRequest = new TokenRequest(email, password);

        var tokenResponse = await _tokensApi.GetTokenAsync(tokenRequest, cancellationToken);
        _tokenStore.SaveToken(tokenResponse.Token);
        IsLoggedIn = true;
    }

    public async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        _tokenStore.RemoveToken();
        IsLoggedIn = false;
        await Task.CompletedTask;
    }

    public async Task<ApplicatonUser> GetUserAsync()
    {
        var userProfileResponse = await _personalApi.GetProfileAsync(default);

        if(!userProfileResponse.IsSuccessStatusCode)
        {
            IsLoggedIn = false;
        }    

        UserName = userProfileResponse.Content?.UserName;
        Email = userProfileResponse.Content?.Email;
        PhoneNumber = userProfileResponse.Content?.PhoneNumber;
        FirstName = userProfileResponse.Content?.FirstName;
        LastName = userProfileResponse.Content?.LastName;

        var user = new ApplicatonUser()
        {
            FirstName = FirstName,
            LastName = LastName,
            UserName = UserName,
            Email = Email,
            PhoneNumber = PhoneNumber,
        };

        return user;
    }
}
