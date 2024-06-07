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
}
