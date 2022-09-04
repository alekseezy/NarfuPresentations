using Mapster;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using NarfuPresentations.Core.Application.Common.Exceptions;
using NarfuPresentations.Core.Application.Identity;
using NarfuPresentations.Core.Application.Identity.Roles;
using NarfuPresentations.Core.Infrastructure.Identity.Models;
using NarfuPresentations.Core.Infrastructure.Persistence.Context;
using NarfuPresentations.Shared.Contracts.Authentication.Constants;
using NarfuPresentations.Shared.Contracts.Identity.Roles.Requests;
using NarfuPresentations.Shared.Contracts.Identity.Roles.Responses;

namespace NarfuPresentations.Core.Infrastructure.Identity.Services;

public class RoleService : IRoleService
{
    private readonly ICurrentUser _currentUser;
    private readonly ApplicationDbContext _dbContext;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleService(
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext dbContext,
        ICurrentUser currentUser)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<string> CreateOrUpdateAsync(CreateOrUpdateRoleRequest request)
    {
        if (string.IsNullOrEmpty(request.Id))
            return await CreateAsync(request);
        return await UpdateAsync(request);
    }

    public async Task<bool> ExistsAsync(string roleName, string? excludeId) =>
        await _roleManager.FindByNameAsync(roleName)
            is { } existingRole
        && existingRole.Id != excludeId;

    public async Task<RoleResponse> GetByIdAsync(string id) =>
        await _dbContext.Roles.SingleOrDefaultAsync(_ => _.Id == id)
            is { } role
            ? role.Adapt<RoleResponse>()
            : throw new NotFoundException("Role Not Found.");

    public async Task<RoleResponse> GetByIdWithPermissionsAsync(string roleId,
        CancellationToken cancellationToken)
    {
        var role = await GetByIdAsync(roleId);

        role.Permissions = await _dbContext
            .RoleClaims
            .Where(claim => claim.RoleId == roleId && claim.ClaimType == ClaimConstants.Permission)
            .Select(claim => claim.ClaimValue)
            .ToListAsync(cancellationToken);

        return role;
    }

    public async Task<List<RoleResponse>> GetListAsync(CancellationToken cancellationToken) =>
        (await _roleManager.Roles.ToListAsync(cancellationToken))
        .Adapt<List<RoleResponse>>();

    public async Task<int> GetCountAsync(CancellationToken cancellationToken) =>
        await _roleManager.Roles.CountAsync(cancellationToken);

    public async Task<string> UpdatePermissionsAsync(UpdateRolePermissionsRequest request,
        CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId);
        _ = role ?? throw new NotFoundException("Role Not Found");

        if (role.Name == RoleConstants.Admin)
            throw new ConflictException("Not allowed to modify Permissions for this Role.");

        var currentClaims = await _roleManager.GetClaimsAsync(role);

        foreach (var claim in currentClaims.Where(
                     cl => request.Permissions.All(p => p != cl.Value)))
        {
            var removeResult = await _roleManager.RemoveClaimAsync(role, claim);

            if (!removeResult.Succeeded)
                throw new InternalServerException("Update Permissions failed.");
        }

        foreach (var permission in request.Permissions.Where(c =>
                     currentClaims.Any(p => p.Value == c)))
            if (!string.IsNullOrEmpty(permission))
            {
                await _dbContext.RoleClaims.AddAsync(new ApplicationRoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = ClaimConstants.Permission,
                    ClaimValue = permission,
                    CreatedBy = _currentUser.GetUserId().ToString()
                }, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

        return "Permissions Successfully Updated.";
    }


    public async Task<string> DeleteAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        _ = role ?? throw new NotFoundException("Role Not Found");

        if (RoleConstants.IsDefault(role.Name))
            throw new ConflictException($"Not allowed to delete {role.Name} Role.");

        if ((await _userManager.GetUsersInRoleAsync(role.Name)).Count > 0)
            throw new ConflictException(
                $"Not allowed to delete {role.Name} Role as it is being used.");

        await _roleManager.DeleteAsync(role);

        return $"Role {role.Name} Successfully Deleted.";
    }

    private async Task<string> CreateAsync(CreateOrUpdateRoleRequest request)
    {
        var role = new ApplicationRole(request.Name, request.Description);
        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
            throw new InternalServerException("Role registration failed.");

        return $"Role {request.Name} was Successfully Created.";
    }

    private async Task<string> UpdateAsync(CreateOrUpdateRoleRequest request)
    {
        var role = await _roleManager.FindByIdAsync(request.Id);

        _ = role ?? throw new NotFoundException("Role Not Found");

        if (RoleConstants.IsDefault(role.Name))
            throw new ConflictException($"Not allowed to modify {role.Name} Role.");

        role.Name = request.Name;
        role.NormalizedName = request.Name.ToUpperInvariant();
        role.Description = request.Description;
        var result = await _roleManager.UpdateAsync(role);

        if (!result.Succeeded)
            throw new InternalServerException("Update role failed");

        return $"Role {request.Name} was Successfully Updated.";
    }
}
