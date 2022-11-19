using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using NarfuPresentations.Core.Application.Identity;
using NarfuPresentations.Core.Infrastructure.Identity.Models;
using NarfuPresentations.Shared.Domain.Common.Contracts;
using NarfuPresentations.Shared.Domain.Entities;

namespace NarfuPresentations.Core.Infrastructure.Persistense.Context;

public class ApplicationDbContext : BaseDbContext
{
    private readonly ICurrentUser _currentUserService;

    public ApplicationDbContext(DbContextOptions options, ICurrentUser currentUserService)
        : base(options, currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public DbSet<Event> Events => Set<Event>();
    public DbSet<Presentation> Presentations => Set<Presentation>();

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
