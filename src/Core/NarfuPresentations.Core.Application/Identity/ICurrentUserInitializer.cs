using System.Security.Claims;

namespace NarfuPresentations.Core.Application.Identity;

public interface ICurrentUserInitializer
{
    void SetCurrentUser(ClaimsPrincipal user);
    void SetCurrentUserId(string userId);
}
