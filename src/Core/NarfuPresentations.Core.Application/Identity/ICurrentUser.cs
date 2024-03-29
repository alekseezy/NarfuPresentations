﻿using NarfuPresentations.Core.Application.Common;

using System.Security.Claims;

namespace NarfuPresentations.Core.Application.Identity;

public interface ICurrentUser : ITransientService
{
    string? Name { get; }
    Guid GetUserId();
    string? GetUserEmail();
    bool IsAuthenticated();
    bool IsInRole(string role);
    IEnumerable<Claim>? GetUserClaims();
}
