using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkoutApp.DAL.Context;
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

        // var user = User.Create(User.DefaultUser, DateTime.UtcNow, DateTime.UtcNow);
        // db.Users.Add(user);
        //
        // var exercises = new List<Exercise>
        // {
        //     Exercise.Create("Squat"),
        //     Exercise.Create("Bench"),
        //     Exercise.Create("Deadlift"),
        // };
        // db.Exercises.AddRange(exercises);
        //
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
