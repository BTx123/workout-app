using FluentResults;
using UnitsNet;
using WorkoutApp.Core.Models;
using WorkoutApp.DAL.Constants;

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

    DistanceType DistanceType { get; set; }

    event EventHandler<DistanceType> DistanceUnitChanged;

    HeightType HeightType { get; set; }

    event EventHandler<HeightType> HeightUnitChanged;

    MassType MassType { get; set; }

    event EventHandler<MassType>? MassUnitChanged;

    OneRepMaxStrategy OneRepMaxStrategy { get; set; }

    event EventHandler<OneRepMaxStrategy>? OneRepMaxStrategyChanged;

    Mass DefaultBarbellWeight { get; set; }

    event EventHandler<Mass>? DefaultBarbellWeightChanged;

    Result Reset();
}