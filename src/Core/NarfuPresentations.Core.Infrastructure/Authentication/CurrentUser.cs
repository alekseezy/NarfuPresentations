﻿using System.Security.Claims;

using NarfuPresentations.Core.Application.Identity;
using NarfuPresentations.Core.Infrastructure.Authentication.Extensions;

namespace NarfuPresentations.Core.Infrastructure.Authentication;

public class CurrentUser : ICurrentUser, ICurrentUserInitializer
{
    private ClaimsPrincipal? _user;

    private Guid _userId = Guid.Empty;

    public string? Name => _user?.Identity?.Name;

    public Guid GetUserId() =>
        IsAuthenticated()
            ? Guid.Parse(_user?.GetUserId() ?? Guid.Empty.ToString())
            : _userId;

    public string? GetUserEmail() =>
        IsAuthenticated()
            ? _user!.GetEmail()
            : string.Empty;

    public bool IsAuthenticated() => _user?.Identity?.IsAuthenticated is true;

    public bool IsInRole(string role) => _user?.IsInRole(role) is true;

    public IEnumerable<Claim>? GetUserClaims() => _user?.Claims;

    public void SetCurrentUser(ClaimsPrincipal user)
    {
        if (_user != null) throw new Exception("Method reserved for in-scope initialization");

        _user = user;
    }

    public void SetCurrentUserId(string userId)
    {
        if (_userId != Guid.Empty)
            throw new Exception("Method reserved for in-scope initialization");

        if (!string.IsNullOrEmpty(userId)) _userId = Guid.Parse(userId);
    }
}
