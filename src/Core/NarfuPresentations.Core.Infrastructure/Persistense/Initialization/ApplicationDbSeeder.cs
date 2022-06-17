using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using NarfuPresentations.Core.Infrastructure.Authentication.Constants;
using NarfuPresentations.Core.Infrastructure.Identity.Models;
using NarfuPresentations.Core.Infrastructure.Persistense.Context;
using NarfuPresentations.Shared.Contracts.Authentication.Constants;

namespace NarfuPresentations.Core.Infrastructure.Persistense.Initialization;

internal class ApplicationDbSeeder
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SeederRunner _seederRunner;
    private readonly ILogger<ApplicationDbSeeder> _logger;

    public ApplicationDbSeeder(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SeederRunner seederRunner, ILogger<ApplicationDbSeeder> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _seederRunner = seederRunner;
        _logger = logger;
    }

    public async Task SeedDatabaseAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        await SeedRolesAsync(dbContext);
        await SeedAdminUserAsync();
        await _seederRunner.RunSeedersAsync(cancellationToken);
    }

    private async Task SeedRolesAsync(ApplicationDbContext dbContext)
    {
        foreach (var roleName in RoleConstants.DefaultRoles)
        {
            if (await _roleManager.Roles.SingleOrDefaultAsync(r => r.Name == roleName)
                is not { } role)
            {
                _logger.LogInformation("Seeding {roleName} Role.", roleName);
                role = new ApplicationRole(roleName);
                await _roleManager.CreateAsync(role);
            }

            switch (roleName)
            {
                case RoleConstants.Basic:
                    await AssignPermissionsToRoleAsync(dbContext, Permissions.Basic, role);
                    break;
                case RoleConstants.Admin:
                    await AssignPermissionsToRoleAsync(dbContext, Permissions.Admin, role);
                    break;
            }
        }
    }

    private async Task AssignPermissionsToRoleAsync(ApplicationDbContext dbContext, IReadOnlyList<Permission> permissions, ApplicationRole role)
    {
        var currentClaims = await _roleManager.GetClaimsAsync(role);

        foreach (var permission in permissions)
        {
            if (currentClaims.Any(c => c.Type == ClaimConstants.Permission && c.Value == permission.Name))
                continue;

            _logger.LogInformation("Seeding {role} Permission '{permission}'", role, permission);
            await dbContext.RoleClaims.AddAsync(new ApplicationRoleClaim
            {
                RoleId = role.Id,
                ClaimType = ClaimConstants.Permission,
                ClaimValue = permission.Name,
                CreatedBy = "ApplicationDbSeeder"
            });
            await dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedAdminUserAsync()
    {
        if (await _userManager.Users.FirstOrDefaultAsync(user => user.Email == UserConstants.AdminDefaults.Email)
            is not { } adminUser)
        {
            var adminUserName = $"{UserConstants.AdminDefaults.UserName}.{RoleConstants.Admin}";
            adminUser = new ApplicationUser
            {
                FirstName = UserConstants.AdminDefaults.UserName,
                LastName = RoleConstants.Admin,
                Email = UserConstants.AdminDefaults.Email,
                UserName = adminUserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = UserConstants.AdminDefaults.Email.ToUpperInvariant(),
                NormalizedUserName = adminUserName.ToUpperInvariant(),
                IsActive = true
            };

            _logger.LogInformation("Seeding Default Admin User");
            var password = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = password.HashPassword(adminUser, UserConstants.AdminDefaults.Password);
            await _userManager.CreateAsync(adminUser);
        }

        if (!await _userManager.IsInRoleAsync(adminUser, RoleConstants.Admin))
        {
            _logger.LogInformation("Assigning Admin Role to Default Admin User");
            await _userManager.AddToRoleAsync(adminUser, RoleConstants.Admin);
        }
    }
}
