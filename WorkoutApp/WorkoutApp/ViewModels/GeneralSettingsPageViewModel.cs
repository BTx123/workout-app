using Microsoft.Extensions.Logging;
using WorkoutApp.DAL.Constants;
using WorkoutApp.Logging;
using WorkoutApp.Services;

namespace WorkoutApp.ViewModels;

public partial class GeneralSettingsPageViewModel : ViewModelBase<GeneralSettingsPageViewModel>
{
    public GeneralSettingsPageViewModel(IDialogService dialogService, ISettingsService settingsService,
        ILogger<GeneralSettingsPageViewModel> logger)
        : base(dialogService, settingsService, logger)
    {
        Title = "General Settings";

        // Update private properties to not trigger PropertyChanged events
        _theme = SettingsService.Theme;
        _themeColor = SettingsService.ThemeColor;
        _keepScreenOn = SettingsService.KeepScreenOn;
        _lockScreenOrientation = SettingsService.LockScreenOrientation;
        _firstDayOfTheWeek = SettingsService.FirstDayOfTheWeek;
        _massType = SettingsService.MassType;
        _distanceType = SettingsService.DistanceType;
        _heightType = SettingsService.HeightType;
        _oneRepMaxStrategy = SettingsService.OneRepMaxStrategy;
    }

    #region Properties

    [ObservableProperty]
    private Theme _theme;

    partial void OnThemeChanged(Theme value)
    {
        SettingsService.Theme = value;
    }

    [ObservableProperty]
    private Color _themeColor;

    partial void OnThemeColorChanged(Color value)
    {
        // FIXME: How to dynamically change theme color at runtime?
        //SettingsService.ThemeColor = value;
    }

    public IReadOnlyList<string> AllThemes { get; } = Enum.GetNames(typeof(Theme));

    [ObservableProperty]
    private bool _keepScreenOn;

    partial void OnKeepScreenOnChanged(bool value)
    {
        SettingsService.KeepScreenOn = value;
    }

    [ObservableProperty]
    private bool _lockScreenOrientation;

    partial void OnLockScreenOrientationChanged(bool value)
    {
        SettingsService.LockScreenOrientation = value;
    }

    [ObservableProperty]
    private DayOfWeek _firstDayOfTheWeek;

    public IReadOnlyList<string> AvailableFirstDayOfTheWeeks { get; } = new List<string>
    {
        DayOfWeek.Sunday.ToString(),
        DayOfWeek.Monday.ToString(),
    };

    partial void OnFirstDayOfTheWeekChanged(DayOfWeek value)
    {
        SettingsService.FirstDayOfTheWeek = value;
    }

    [ObservableProperty]
    private MassType _massType;

    public IReadOnlyList<string> AvailableMassUnits { get; } = Enum.GetNames<MassType>();

    partial void OnMassTypeChanged(MassType value)
    {
        SettingsService.MassType = value;
    }

    [ObservableProperty]
    private DistanceType _distanceType;

    public IReadOnlyList<string> AvailableDistanceUnits { get; } = Enum.GetNames<DistanceType>();

    partial void OnDistanceTypeChanged(DistanceType value)
    {
        SettingsService.DistanceType = value;
    }

    [ObservableProperty]
    private HeightType _heightType;

    public IReadOnlyList<string> AvailableHeightUnits { get; } = Enum.GetNames<HeightType>();

    partial void OnHeightTypeChanged(HeightType value)
    {
        SettingsService.HeightType = value;
    }

    [ObservableProperty]
    private OneRepMaxStrategy _oneRepMaxStrategy;

    public IReadOnlyList<string> AvailableOneRepMaxStrategies { get; } = Enum.GetNames<OneRepMaxStrategy>();

    partial void OnOneRepMaxStrategyChanged(OneRepMaxStrategy value)
    {
        SettingsService.OneRepMaxStrategy = value;
    }

    #endregion

    #region Commands

    [RelayCommand]
    private async Task ShowThemeColorDialog(CancellationToken cancellationToken = default)
    {
        using var _ = Logger.WithCallerScope();

        try
        {
            ThemeColor = await DialogService.DisplayColorDialogAsync(ThemeColor, cancellationToken);
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Unexpected error occurred while showing theme color dialog");
        }
    }

    #endregion
}