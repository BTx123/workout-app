using UnitsNet.Units;
using WorkoutApp.Core.Constants;
using MassUnit = UnitsNet.Units.MassUnit;

namespace WorkoutApp.Core.Models.Settings;

[Serializable]
public class GeneralSettings
{
    /// <summary>
    /// Keep the screen on when a workout is active.
    /// </summary>
    public bool KeepScreenOn { get; set; } = true;

    /// <summary>
    /// Theme to use.
    /// </summary>
    public Theme Theme { get; set; } = Theme.System;

    /// <summary>
    /// Lock screen orientation.
    /// </summary>
    public bool LockScreenOrientation { get; set; } = true;

    /// <summary>
    /// First day of the week for calendars.
    /// </summary>
    public DayOfWeek FirstDayOfTheWeek { get; set; } = DayOfWeek.Sunday;

    /// <summary>
    /// Weight unit to use.
    /// </summary>
    public MassUnit WeightUnit { get; set; } = MassUnit.Pound;

    /// <summary>
    /// Length unit to use.
    /// </summary>
    public LengthUnit DistanceUnit { get; set; } = LengthUnit.Mile;

    /// <summary>
    /// Length unit to use for height.
    /// </summary>
    public LengthUnit HeightUnit { get; set; } = LengthUnit.Inch;
}