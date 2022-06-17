using Microsoft.EntityFrameworkCore;

using NarfuPresentations.Core.Application.Identity;
using NarfuPresentations.Shared.Domain.Common.Contracts;

namespace NarfuPresentations.Core.Infrastructure.Persistense.Context;

public class ApplicationDbContext : BaseDbContext
{
    private readonly ICurrentUser _currentUserService;

    public ApplicationDbContext(DbContextOptions options, ICurrentUser currentUserService)
        : base(options, currentUserService)
    {
        _currentUserService = currentUserService;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var currentUserId = _currentUserService.GetUserId();

        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = currentUserId;
                    entry.Entity.LastModifiedBy = currentUserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModified = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = currentUserId;
                    break;
                case EntityState.Deleted:
                    if (entry is ISoftDelete softDelete)
                    {
                        softDelete.DeletedBy = currentUserId;
                        softDelete.DeletedOn = DateTime.UtcNow;
                        entry.State = EntityState.Modified;
                    }
                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                default:
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
