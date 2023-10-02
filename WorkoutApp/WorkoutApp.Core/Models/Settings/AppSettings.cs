using System.Drawing;
using UnitsNet;
using WorkoutApp.Core.Constants;
using WeightUnit = WorkoutApp.Core.Constants.WeightUnit;

namespace WorkoutApp.Core.Models.Settings;

/// <summary>
/// Application level settings.
/// </summary>
[Serializable]
public class AppSettings
{
    /// <summary>
    /// Default app settings instance.
    /// </summary>
    [NonSerialized]
    public static readonly AppSettings Default = new();

    #region General Settings

    /// <summary>
    /// Theme to use.
    /// </summary>
    public Theme Theme { get; set; } = Theme.System;

    /// <summary>
    /// Theme color to use.
    /// </summary>
    public Color ThemeColor { get; set; } = ColorTranslator.FromHtml("#512BD4");

    /// <summary>
    /// Keep the screen on when a workout is active.
    /// </summary>
    public bool KeepScreenOn { get; set; } = true;

    /// <summary>
    /// Lock screen orientation.
    /// </summary>
    public bool LockScreenOrientation { get; set; } = true;

    /// <summary>
    /// First day of the week for calendars.
    /// </summary>
    public DayOfWeek FirstDayOfTheWeek { get; set; } = DayOfWeek.Sunday;

    /// <summary>
    /// Mass unit to use.
    /// </summary>
    public WeightUnit MassUnit { get; set; } = WeightUnit.Pound;

    /// <summary>
    /// Length unit to use.
    /// </summary>
    public DistanceUnit DistanceUnit { get; set; } = DistanceUnit.Mile;

    /// <summary>
    /// Length unit to use for height.
    /// </summary>
    public HeightUnit HeightUnit { get; set; } = HeightUnit.Inch;

    /// <summary>
    /// One repetition max strategy to use.
    /// </summary>
    public OneRepMaxStrategy OneRepMaxStrategy { get; set; } = OneRepMaxStrategy.Brzycki;

    #endregion

    #region Gym Settings

    /// <summary>
    /// Available barbells.
    /// </summary>
    public ICollection<IBarbell> AvailableBarbells { get; } = new List<IBarbell>
    {
        Barbell.StandardBarbell,
        Barbell.OlympicBarbell,
        Barbell.SafetySquatBar,
        Barbell.EzCurlBar
    };

    public Barbell DefaultBarbell { get; set; } = Barbell.StandardBarbell;

    /// <summary>
    /// Available plates for loading barbells.
    /// </summary>
    public IDictionary<Mass, int> AvailablePlates { get; } = new Dictionary<Mass, int>
    {
        { Mass.FromPounds(45), 10 },
        { Mass.FromPounds(35), 10 },
        { Mass.FromPounds(25), 10 },
        { Mass.FromPounds(10), 10 },
        { Mass.FromPounds(5), 10 },
        { Mass.FromPounds(2.5), 10 },
    };

    #endregion

    #region Workout Settings

    /// <summary>
    /// Show assistance exercises.
    /// </summary>
    public bool ShowAssistanceExercises { get; set; } = true;

    /// <summary>
    /// Show reps to beat 1RM.
    /// </summary>
    public bool ShowRepsToBeat1Rm { get; set; } = true;

    /// <summary>
    /// Show reps to beat PR.
    /// </summary>
    public bool ShowRepsToBeatPr { get; set; } = true;

    /// <summary>
    /// Deadlift supination strategy.
    /// </summary>
    public SupinationStrategy SupinationStrategy { get; set; } = SupinationStrategy.None;

    #endregion
}

public static class ColorExtensions
{
    public static string ToArgbString(this Color color)
    {
        return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
    }
}