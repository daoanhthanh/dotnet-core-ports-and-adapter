using Adapter.Out.Postgres.User;
using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Adapter.Out.Postgres;

public class PostgresRootDbContext(DbContextOptions<PostgresRootDbContext> options)
    : DbContext(options)
{
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        
        modelBuilder.ApplyConfiguration(new UserPasswordMap());

        base.OnModelCreating(modelBuilder);
    }

    private void OnBeforeSaving()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is EntityAudit)
            .ToList();
        UpdateSoftDelete(entities);
        UpdateTimestamps(entities);
    }

    private void UpdateSoftDelete(List<EntityEntry> entries)
    {
        var filtered = entries
            .Where(x => x.State == EntityState.Added
                        || x.State == EntityState.Deleted);

        foreach (var entry in filtered)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    // entry.CurrentValues["IsDeleted"] = false;
                    ((EntityAudit)entry.Entity).IsDeleted = false;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;

                    // entry.CurrentValues["IsDeleted"] = true;
                    ((EntityAudit)entry.Entity).IsDeleted = true;
                    break;
            }
        }
    }

    private void UpdateTimestamps(List<EntityEntry> entries)
    {
        var filtered = entries
            .Where(x => x.State == EntityState.Added
                        || x.State == EntityState.Modified);

        // TODO: Get real current user id
        var currentUserId = 1;

        foreach (var entry in filtered)
        {
            if (entry.State == EntityState.Added)
            {
                ((EntityAudit)entry.Entity).CreatedAt = DateTime.UtcNow;
                ((EntityAudit)entry.Entity).CreatedBy = currentUserId;
            }

            ((EntityAudit)entry.Entity).UpdatedAt = DateTime.UtcNow;
            ((EntityAudit)entry.Entity).UpdatedBy = currentUserId;
        }
    }
}