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

        return base.SaveChanges();
    }
}
