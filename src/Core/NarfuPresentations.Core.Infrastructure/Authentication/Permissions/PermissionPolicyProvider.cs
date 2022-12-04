﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using NarfuPresentations.Shared.Contracts.Authentication.Constants;

namespace NarfuPresentations.Core.Infrastructure.Authentication.Permissions;

internal class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options) =>
        FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);

    public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
        FallbackPolicyProvider.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() =>
        Task.FromResult<AuthorizationPolicy>(null!)!;

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (!policyName.StartsWith(ClaimConstants.Permission, StringComparison.OrdinalIgnoreCase))
            return FallbackPolicyProvider.GetPolicyAsync(policyName);

        var policy = new AuthorizationPolicyBuilder();
        policy.AddRequirements(new PermissionRequirement(policyName));

        return Task.FromResult<AuthorizationPolicy?>(policy.Build());
    }
}
