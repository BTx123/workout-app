using FluentResults;
using Microsoft.Extensions.Logging;
using WorkoutApp.Core.Constants;
using WorkoutApp.Core.Models.Settings;

namespace WorkoutApp.Services;

public class PreferencesSettingsService : ServiceBase<PreferencesSettingsService>, ISettingsService
{
    public override string Name => "preferences-settings-service";

    public PreferencesSettingsService(ILogger<PreferencesSettingsService> logger) : base(logger)
    {
#if DEBUG
        // var result = Reset();
        // if (!result.IsSuccess) logger.LogError("Failed to reset settings for debug.");
#endif
    }

    #region General Settings

    public Theme Theme
    {
        get
        {
            var s = Preferences.Get(SettingsKey.Theme, null);
            return Enum.TryParse(s, out Theme appTheme) ? appTheme : AppSettings.Default.Theme;
        }
        set
        {
            Preferences.Set(SettingsKey.Theme, value.ToString());
            Logger.LogDebug("{ServiceName}: {Setting} updated to {Value}", Name, nameof(Theme), value);
            ThemeChanged?.Invoke(this, value);
        }
    }

    public event EventHandler<Theme>? ThemeChanged;

    public Color ThemeColor
    {
        get
        {
            var s = Preferences.Get(SettingsKey.ThemeColor, null);
            if (s == null) return Color.FromArgb(AppSettings.Default.ThemeColor.ToArgbString());
            return Color.FromArgb(s);
        }
        set
        {
            Preferences.Set(SettingsKey.ThemeColor, value.ToArgbHex());
            Logger.LogDebug("{ServiceName}: {Setting} updated to {Value}", Name, nameof(ThemeColor), value);
            ThemeColorChanged?.Invoke(this, value);
        }
    }

    public event EventHandler<Color>? ThemeColorChanged;

    public bool KeepScreenOn
    {
        get => Preferences.Get(SettingsKey.KeepScreenOn, AppSettings.Default.KeepScreenOn);
        set
        {
            Preferences.Set(SettingsKey.KeepScreenOn, value);
            Logger.LogDebug("{ServiceName}: {Setting} updated to {Value}", Name, nameof(KeepScreenOn), value);
            KeepScreenOnChanged?.Invoke(this, value);
        }
    }

    public event EventHandler<bool>? KeepScreenOnChanged;

    public bool LockScreenOrientation
    {
        get => Preferences.Get(SettingsKey.LockScreenOrientation, AppSettings.Default.LockScreenOrientation);
        set
        {
            Preferences.Set(SettingsKey.LockScreenOrientation, value);
            Logger.LogDebug("{ServiceName}: {Setting} updated to {Value}", Name, nameof(LockScreenOrientation), value);
            LockScreenOrientationChanged?.Invoke(this, value);
        }
    }

    public event EventHandler<bool>? LockScreenOrientationChanged;

    public DayOfWeek FirstDayOfTheWeek
    {
        get
        {
            var s = Preferences.Get(SettingsKey.FirstDayOfTheWeek, null);
            return Enum.TryParse(s, out DayOfWeek value) ? value : AppSettings.Default.FirstDayOfTheWeek;
        }
        set
        {
            Preferences.Set(SettingsKey.FirstDayOfTheWeek, value.ToString());
            Logger.LogDebug("{ServiceName}: {Setting} updated to {Value}", Name, nameof(FirstDayOfTheWeek), value);
            FirstDayOfWeekChanged?.Invoke(this, value);
        }
    }

    public event EventHandler<DayOfWeek>? FirstDayOfWeekChanged;

    public DistanceUnit DistanceUnit
    {
        get
        {
            var s = Preferences.Get(SettingsKey.DistanceUnit, null);
            return Enum.TryParse(s, out DistanceUnit value) ? value : AppSettings.Default.DistanceUnit;
        }
        set
        {
            Preferences.Set(SettingsKey.DistanceUnit, value.ToString());
            Logger.LogDebug("{ServiceName}: {Setting} updated to {Value}", Name, nameof(DistanceUnit), value);
            DistanceUnitChanged?.Invoke(this, value);
        }
    }

    public event EventHandler<DistanceUnit>? DistanceUnitChanged;

    public HeightUnit HeightUnit
    {
        get
        {
            var s = Preferences.Get(SettingsKey.HeightUnit, null);
            return Enum.TryParse(s, out HeightUnit value) ? value : AppSettings.Default.HeightUnit;
        }
        set
        {
            Preferences.Set(SettingsKey.HeightUnit, value.ToString());
            Logger.LogDebug("{ServiceName}: {Setting} updated to {Value}", Name, nameof(HeightUnit), value);
            HeightUnitChanged?.Invoke(this, value);
        }
    }

    public event EventHandler<HeightUnit>? HeightUnitChanged;

    public WeightUnit MassUnit
    {
        get
        {
            var s = Preferences.Get(SettingsKey.MassUnit, null);
            return Enum.TryParse(s, out WeightUnit value) ? value : AppSettings.Default.MassUnit;
        }
        set
        {
            Preferences.Set(SettingsKey.MassUnit, value.ToString());
            Logger.LogDebug("{ServiceName}: {Setting} updated to {Value}", Name, nameof(MassUnit), value);
            MassUnitChanged?.Invoke(this, value);
        }
    }

    public event EventHandler<WeightUnit>? MassUnitChanged;

    public OneRepMaxStrategy OneRepMaxStrategy
    {
        get
        {
            var s = Preferences.Get(SettingsKey.OneRepMaxStrategy, null);
            return Enum.TryParse(s, out OneRepMaxStrategy value) ? value : AppSettings.Default.OneRepMaxStrategy;
        }
        set
        {
            Preferences.Set(SettingsKey.MassUnit, value.ToString());
            Logger.LogDebug("{ServiceName}: {Setting} updated to {Value}", Name, nameof(OneRepMaxStrategy), value);
            OneRepMaxStrategyChanged?.Invoke(this, value);
        }
    }

    public event EventHandler<OneRepMaxStrategy>? OneRepMaxStrategyChanged;

    #endregion

    public Result Reset()
    {
        try
        {
            Theme = AppSettings.Default.Theme;
            KeepScreenOn = AppSettings.Default.KeepScreenOn;
            LockScreenOrientation = AppSettings.Default.LockScreenOrientation;

            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}