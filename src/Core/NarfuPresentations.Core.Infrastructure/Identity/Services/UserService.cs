using Ardalis.Specification.EntityFrameworkCore;

using Mapster;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using NarfuPresentations.Core.Application.Authentication.Exceptions;
using NarfuPresentations.Core.Application.Common.Exceptions;
using NarfuPresentations.Core.Application.Common.FileStorage;
using NarfuPresentations.Core.Application.Identity.Users;
using NarfuPresentations.Core.Infrastructure.Common.Specification;
using NarfuPresentations.Core.Infrastructure.Identity.Models;
using NarfuPresentations.Core.Infrastructure.Persistense.Context;
using NarfuPresentations.Shared.Contracts.Authentication.Constants;
using NarfuPresentations.Shared.Contracts.Common;
using NarfuPresentations.Shared.Contracts.Identity.Users.Filters;
using NarfuPresentations.Shared.Contracts.Identity.Users.Requests;
using NarfuPresentations.Shared.Contracts.Identity.Users.Responses;
using NarfuPresentations.Shared.Domain.Common.Enums;

namespace NarfuPresentations.Core.Infrastructure.Identity.Services;

public class UserService : IUserService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _context;

    private readonly IFileStorageService _fileStorage;

    public UserService(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ApplicationDbContext context,
        IFileStorageService fileStorage)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
        _fileStorage = fileStorage;
    }

    public async Task<bool> ExistsWithEmailAsync(string email) =>
        await _userManager.FindByEmailAsync(email) is not null;
    public async Task<bool> ExistsWithPhoneNumberAsync(string phone) =>
        await _userManager.Users.Where(user => user.PhoneNumber == phone).AnyAsync();
    public async Task<bool> ExistsWithUserNameAsync(string userName) =>
        await _userManager.FindByNameAsync(userName) is not null;

    public async Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken = default)
    {
        var permissions = await GetPermissionsAsync(userId, cancellationToken);

        return permissions?.Contains(permission) ?? false;
    }

    public async Task<PaginationResponse<UserDetailsResponse>> SearchAsync(UsersFilter filter, CancellationToken cancellationToken)
    {
        var specification = new EntitiesByPaginationFilterSpecification<ApplicationUser>(filter);

        var users = await _userManager
            .Users
            .WithSpecification(specification)
            .ProjectToType<UserDetailsResponse>()
            .ToListAsync(cancellationToken);
        var count = await _userManager
            .Users
            .CountAsync(cancellationToken);

        return new PaginationResponse<UserDetailsResponse>(users, count, filter.PageNumber, filter.PageSize);
    }

    public async Task<string> CreateAsync(CreateUserRequest request, string origin, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber,
            IsActive = true
        };

        var creationResult = await _userManager.CreateAsync(user, request.Password);
        if (!creationResult.Succeeded)
            throw new InternalServerException("Validation Errors Occured.");

        await _userManager.AddToRoleAsync(user, RoleConstants.Basic);

        var messages = new List<string> { $"User {user.UserName} Registered." };

        return string.Join(Environment.NewLine, messages);
    }

    public async Task UpdateAsync(UpdateUserRequest request, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        _ = user ?? throw new NotFoundException("User Not Found.");

        var currentRelativeImagePath = user.ImageUrl ?? string.Empty;
        if (request.Image != null || request.DeleteCurrentImage)
        {
            user.ImageUrl = await _fileStorage.UploadAsync<ApplicationUser>(request.Image, FileType.Image);
            if (request.DeleteCurrentImage && !string.IsNullOrEmpty(currentRelativeImagePath))
            {
                _fileStorage.TryRemove(currentRelativeImagePath);
            }
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        var currentPhoneNumber = await _userManager.GetPhoneNumberAsync(user);
        if (request.PhoneNumber != currentPhoneNumber)
            await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);

        var updationResult = await _userManager.UpdateAsync(user);

        await _signInManager.RefreshSignInAsync(user);

        if (!updationResult.Succeeded)
            throw new InternalServerException("Update profile failed.");
    }

    public async Task<IEnumerable<UserDetailsResponse>> GetAllAsync(CancellationToken cancellationToken) =>
        (await _userManager
                .Users
                .AsNoTracking()
                .ToListAsync(cancellationToken))
            .Adapt<List<UserDetailsResponse>>();
    public async Task<UserDetailsResponse> GetAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager
            .Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException("User Not Found.");

        return user.Adapt<UserDetailsResponse>();
    }

    public async Task<IEnumerable<string>> GetPermissionsAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId);
        _ = user ?? throw new UnauthorizedException("Authentication Failed.");

        var userRoles = await _userManager.GetRolesAsync(user);
        var permissions = new List<string>();

        foreach (var role in await _roleManager
            .Roles
            .Where(r => userRoles.Contains(r.Name))
            .ToListAsync(cancellationToken))
        {
            permissions.AddRange(await _context
                .RoleClaims
                .Where(rc => rc.RoleId == role.Id && rc.ClaimType == ClaimConstants.Permission)
                .Select(rc => rc.ClaimValue)
                .ToListAsync(cancellationToken));
        }

        return permissions.Distinct();
    }

    public async Task<IEnumerable<UserRoleResponse>> GetRolesAsync(string userId, CancellationToken cancellationToken)
    {
        var userRoles = new List<UserRoleResponse>();

        var user = await _userManager.FindByIdAsync(userId);
        var applicationRoles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);

        foreach (var role in applicationRoles)
        {
            userRoles.Add(new()
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Enabled = await _userManager.IsInRoleAsync(user, role.Name)
            });
        }

        return userRoles;
    }

    public async Task<string> AssignRolesAsync(string userId, AssignUserRolesRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException("User Nor Found.");

        foreach (var userRole in request.UserRoles)
        {
            if (await _roleManager.FindByNameAsync(userRole.RoleName) is not null)
            {
                if (userRole.Enabled)
                {
                    if (!await _userManager.IsInRoleAsync(user, userRole.RoleName))
                    {
                        await _userManager.AddToRoleAsync(user, userRole.RoleName);
                    }
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, userRole.RoleName);
                }
            }
        }

        return "User Roles Updated Successfully.";
    }
    public Task<string> ConfirmEmailAsync(string userId, string code) =>
        throw new NotImplementedException();
}
