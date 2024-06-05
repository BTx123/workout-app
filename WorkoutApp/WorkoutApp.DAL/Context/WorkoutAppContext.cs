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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        optionsBuilder
            .UseSqlite("Data Source=/Users/btom/src/workout-app/WorkoutApp/WorkoutApp.DAL/database.db")
            .UseValidationCheckConstraints();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppSetting>(entity =>
        {
            entity.ToTable("app_settings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DistanceUnit).HasColumnName("distance_unit");
            entity.Property(e => e.FirstDayOfWeek)
                .HasDefaultValue(AppSetting.Default.FirstDayOfWeek)
                .HasColumnName("first_day_of_week")
                .HasConversion<string>();
            entity.Property(e => e.HeightUnit)
                .HasDefaultValue(AppSetting.Default.HeightUnit)
                .HasColumnName("height_unit")
                .HasConversion<string>();
            entity.Property(e => e.KeepScreenOn)
                .HasDefaultValue(AppSetting.Default.KeepScreenOn)
                .HasColumnName("keep_screen_on");
            entity.Property(e => e.LockScreenOrientation)
                .HasDefaultValue(AppSetting.Default.LockScreenOrientation)
                .HasColumnName("lock_screen_orientation");
            entity.Property(e => e.OneRepMaxStrategy)
                .HasDefaultValue(AppSetting.Default.OneRepMaxStrategy)
                .HasColumnName("one_rep_max_strategy")
                .HasConversion<string>();
            entity.Property(e => e.ShowAssistanceExercises)
                .HasDefaultValue(AppSetting.Default.ShowAssistanceExercises)
                .HasColumnName("show_assistance_exercises");
            entity.Property(e => e.ShowRepsToBeat1Rm)
                .HasDefaultValue(AppSetting.Default.ShowRepsToBeat1Rm)
                .HasColumnName("show_reps_to_beat_1rm");
            entity.Property(e => e.ShowRepsToBeatPr)
                .HasDefaultValue(AppSetting.Default.ShowRepsToBeatPr)
                .HasColumnName("show_reps_to_beat_pr");
            entity.Property(e => e.SupinationStrategy)
                .HasDefaultValue(AppSetting.Default.SupinationStrategy)
                .HasColumnName("supination_strategy")
                .HasConversion<string>();
            entity.Property(e => e.Theme)
                .HasDefaultValue(AppSetting.Default.Theme)
                .HasColumnName("theme")
                .HasConversion<string>();
            entity.Property(e => e.WeightUnit)
                .HasDefaultValue(AppSetting.Default.WeightUnit)
                .HasColumnName("weight_unit")
                .HasConversion<string>();
        });

        modelBuilder.Entity<Barbell>(entity =>
        {
            entity.ToTable("barbells");

            entity.HasIndex(e => e.Name, "IX_barbells_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.MassKg)
                .HasDefaultValueSql("0")
                .HasColumnName("weight_kg");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.ToTable("exercises");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Barbell).HasColumnName("barbell");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Plate>(entity =>
        {
            entity.ToTable("plates");

            entity.HasIndex(e => e.MassKg, "IX_plates_weight_kg").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MassKg)
                .HasDefaultValueSql("0")
                .HasColumnName("weight_kg");
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.ToTable("workouts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Note).HasColumnName("notes");
            entity.Property(e => e.StartedAt).HasColumnName("started_at").HasConversion<string>();
            entity.Property(e => e.StoppedAt).HasColumnName("stopped_at").HasConversion<string>();
        });
    }
}
