using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UnitsNet;
using WorkoutApp.DAL.Context;
using WorkoutApp.DAL.Entities;
using WorkoutApp.Services;

namespace WorkoutApp;

public partial class AppShell
{
    private static readonly Random Random = new();

    private readonly IDbContextFactory<WorkoutAppContext> _dbContextFactory;
    private readonly ISettingsService _settingsService;
    private readonly IDeviceDisplay _deviceDisplay;
    private readonly IDisplayOrientationService _displayOrientationService;
    private readonly ILogger<AppShell> _logger;

    public AppShell(
        IDbContextFactory<WorkoutAppContext> dbContextFactory,
        ISettingsService settingsService,
        IDeviceDisplay deviceDisplay,
        IDisplayOrientationService displayOrientationService,
        ILogger<AppShell> logger)
    {
        InitializeComponent();

        _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        _deviceDisplay = deviceDisplay ?? throw new ArgumentNullException(nameof(deviceDisplay));
        _displayOrientationService = displayOrientationService ??
                                     throw new ArgumentNullException(nameof(displayOrientationService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _settingsService.KeepScreenOnChanged += SettingsServiceOnKeepScreenOnChanged;
        _settingsService.LockScreenOrientationChanged += SettingsServiceOnLockScreenOrientationChanged;
    }

    protected override void OnAppearing()
    {
        var keepScreenOn = _settingsService.KeepScreenOn;
        LoadKeepScreenOn(keepScreenOn);

        var lockScreenOrientation = _settingsService.LockScreenOrientation;
        LoadLockScreenOrientation(lockScreenOrientation);

#if DEBUG
        // IEnumerable<Set> GetRandomSets(SetGroup group)
        // {
        //     var sets = new List<Set>();
        //
        //     var setCount = Random.NextInt64(1, 10);
        //     for (var i = 0; i < setCount; i++)
        //     {
        //         var repetitions = Random.Next(8, 12);
        //         var weight = Mass.FromPounds(Random.Next(45, 315));
        //         var set = Set.Create(i, repetitions, weight.ToUnit(MassUnit.Kilogram).Value, DateTime.UtcNow, DateTime.UtcNow, group);
        //         set.IsAmrap = Random.NextSingle() >= 0.5;
        //         set.IsDone = Random.NextSingle() >= 0.5;
        //         sets.Add(set);
        //     }
        //
        //     return sets;
        // }

        _logger.LogInformation("Loading sample data...");

        using var db = _dbContextFactory.CreateDbContext();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        using var transaction = db.Database.BeginTransaction();

        try
        {
            var barbells = new List<Barbell>
            {
                new() { Name = "Olympic", MassKg = Convert.ToDecimal(Mass.FromKilograms(20).Kilograms) },
                new() { Name = "Standard", MassKg = Convert.ToDecimal(Mass.FromPounds(44).Kilograms) }
            };
            db.Barbells.AddRange(barbells);
            db.SaveChanges();

            var standardBarbell = db.Barbells.FirstOrDefault(b => b.Name == "Standard");
            if (standardBarbell == null) throw new Exception("Failed to get standard barbell for sample data set");
            standardBarbell.MassKg = Convert.ToDecimal(Mass.FromPounds(45).Kilograms);
            Thread.Sleep(2000);
            db.Barbells.Update(standardBarbell);
            db.SaveChanges();

            var exercises = new List<Exercise>
            {
                new() { Name = "Squat", Barbell = standardBarbell },
                new() { Name = "Bench", Barbell = standardBarbell },
                new() { Name = "Deadlift", Barbell = standardBarbell },
            };
            db.Exercises.AddRange(exercises);
            db.SaveChanges();

            transaction.Commit();
        }
        catch (Exception e)
        {
            _logger.LogError("Unexpected error occurred: {Exception}", e);
        }

        // const int workoutCount = 10;
        // for (var i = 0; i < workoutCount; i++)
        // {
        //     var workoutNumber = i + 1;
        //
        //     var workout = Workout.Create(DateTime.UtcNow - TimeSpan.FromDays(i), $"Workout {workoutNumber}",DateTime.UtcNow, DateTime.UtcNow, user);
        //     workout.Note = $"This is a note for workout {workoutNumber}";
        //     workout.DurationNs = Duration.FromHours(2).ToUnit(DurationUnit.Nanosecond).Value;
        //     foreach (var exercise in exercises)
        //     {
        //         var group = SetGroup.Create(workout);
        //         group.Exercise = exercise;
        //         var sets = GetRandomSets(group);
        //         foreach (var set in sets)
        //         {
        //             group.Sets.Add(set);
        //         }
        //         workout.SetGroups.Add(group);
        //     }
        //
        //     db.Workouts.Add(workout);
        // }
        //
        // db.SaveChanges();
        //
        // _logger.LogInformation("Finished loading sample data");
#endif
    }

    #region Event Handlers

    private void SettingsServiceOnKeepScreenOnChanged(object? sender, bool e)
    {
        LoadKeepScreenOn(e);
    }

    private void SettingsServiceOnLockScreenOrientationChanged(object? sender, bool e)
    {
        LoadLockScreenOrientation(e);
    }

    #endregion

    #region Helpers

    private void LoadKeepScreenOn(bool keepScreenOn)
    {
        _deviceDisplay.KeepScreenOn = keepScreenOn;
    }

    private void LoadLockScreenOrientation(bool lockScreenOrientation)
    {
        _displayOrientationService.SetDisplayOrientationLock(lockScreenOrientation);
    }

    #endregion
}
