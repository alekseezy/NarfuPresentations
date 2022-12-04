using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using NarfuPresentations.Core.Application.Identity;
using NarfuPresentations.Core.Infrastructure.Identity.Models;
using NarfuPresentations.Core.Infrastructure.Persistence.Extensions;
using NarfuPresentations.Shared.Domain.Common.Contracts;

namespace NarfuPresentations.Core.Infrastructure.Persistence.Context;

public abstract class BaseDbContext :
    IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
        IdentityUserRole<string>, IdentityUserLogin<string>, ApplicationRoleClaim,
        IdentityUserToken<string>>
{
    private readonly ICurrentUser _currentUserService;

    protected BaseDbContext(DbContextOptions options, ICurrentUser currentUserService)
        : base(options) =>
        _currentUserService = currentUserService;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        builder.ApplyGlobalFilters<ISoftDelete>(s => s.DeletedOn == null);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }
}
