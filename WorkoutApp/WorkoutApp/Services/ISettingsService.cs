using FluentResults;
using WorkoutApp.Core.Constants;

namespace WorkoutApp.Services;

public interface ISettingsService : IService
{
    Theme Theme { get; set; }

    event EventHandler<Theme> ThemeChanged;

    Color ThemeColor { get; set; }

    event EventHandler<Color> ThemeColorChanged;

    bool KeepScreenOn { get; set; }

    event EventHandler<bool> KeepScreenOnChanged;

    bool LockScreenOrientation { get; set; }

    event EventHandler<bool> LockScreenOrientationChanged;

    DayOfWeek FirstDayOfTheWeek { get; set; }

    event EventHandler<DayOfWeek> FirstDayOfWeekChanged;

    DistanceUnit DistanceUnit { get; set; }

    event EventHandler<DistanceUnit> DistanceUnitChanged;

    HeightUnit HeightUnit { get; set; }

    event EventHandler<HeightUnit> HeightUnitChanged;

    WeightUnit MassUnit { get; set; }

    event EventHandler<WeightUnit>? MassUnitChanged;

    OneRepMaxStrategy OneRepMaxStrategy { get; set; }

    event EventHandler<OneRepMaxStrategy> OneRepMaxStrategyChanged;

    Result Reset();
}