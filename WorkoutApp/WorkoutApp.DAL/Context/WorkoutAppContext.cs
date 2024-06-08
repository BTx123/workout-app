using Microsoft.EntityFrameworkCore;
using WorkoutApp.DAL.Entities;

namespace WorkoutApp.DAL.Context;

public class WorkoutAppContext : DbContext
{
    public WorkoutAppContext()
    {
    }

    public WorkoutAppContext(DbContextOptions<WorkoutAppContext> options)
        : base(options)
    {
    }

    public DbSet<AppSetting> AppSettings { get; set; }

    public DbSet<Barbell> Barbells { get; set; }

    public DbSet<Plate> Plates { get; set; }

    public DbSet<Exercise> Exercises { get; set; }

    public DbSet<Workout> Workouts { get; set; }

    public DbSet<SetGroup> SetGroups { get; set; }

    public DbSet<Set> Sets { get; set; }

    public override int SaveChanges()
    {
        SetChangedEntitiesCreatedUpdatedAt();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetChangedEntitiesCreatedUpdatedAt();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SetChangedEntitiesCreatedUpdatedAt();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        SetChangedEntitiesCreatedUpdatedAt();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    /// Updates changed <see cref="EntityBase"/> entities with
    /// <see cref="EntityBase.CreatedAt"/> and <see cref="EntityBase.UpdatedAt"/> timestamps.
    /// </summary>
    /// <remarks>
    /// See: https://www.entityframeworktutorial.net/faq/set-created-and-modified-date-in-efcore.aspx
    /// </remarks>
    private void SetChangedEntitiesCreatedUpdatedAt()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is EntityBase)
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            var timestamp = DateTime.UtcNow;
            if (entityEntry.Entity is not EntityBase entity) continue;

            entity.UpdatedAt = timestamp;
            if (entityEntry.State == EntityState.Added)
            {
                entity.CreatedAt = timestamp;
            }
        }
    }
}
