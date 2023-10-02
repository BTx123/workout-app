using Microsoft.Extensions.Logging;
using UnitsNet;
using UnitsNet.Units;
using WorkoutApp.Constants;
using WorkoutApp.Core.Constants;
using WorkoutApp.Services;
using WeightUnit = WorkoutApp.Core.Constants.WeightUnit;

namespace WorkoutApp.ViewModels;

public partial class MainPageViewModel : ViewModelBase<MainPageViewModel>
{
    public MainPageViewModel(IDialogService dialogService,
        ISettingsService settingsService, ILogger<MainPageViewModel> logger)
        : base(dialogService, settingsService, logger)
    {
        SettingsService.DistanceUnitChanged += SettingsServiceOnDistanceUnitChanged;
        SettingsService.HeightUnitChanged += SettingsServiceOnHeightUnitChanged;
        SettingsService.MassUnitChanged += SettingsServiceOnMassUnitChanged;

        UpdateUnits();
    }

    #region Properties

    public string Text
    {
        get
        {
            return Count switch
            {
                0 => "Click Me!",
                1 => $"Clicked {Count} time",
                _ => $"Clicked {Count} times"
            };
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Text))]
    private int _count;
        
    [ObservableProperty]
    private Length _height = Length.FromFeet(5);

    [ObservableProperty]
    private Length _distance = Length.FromMiles(1);

    [ObservableProperty]
    private Mass _mass = Mass.FromPounds(150);

    #endregion

    #region Commands

    [RelayCommand]
    public void Click()
    {
        Count++;
    }

    [RelayCommand]
    public void Reset()
    {
        Count = 0;
    }

    [RelayCommand]
    public async Task NavigateTo1RmCalculator(CancellationToken cancellationToken = default)
    {
        await Shell.Current.GoToAsync(RouteKeys.OneRepMaxCalculator);
    }

    #endregion

    #region Event Handlers

    private void SettingsServiceOnDistanceUnitChanged(object? sender, DistanceUnit e)
    {
        UpdateDistanceUnits(e.ToLengthUnit());
    }

    private void SettingsServiceOnHeightUnitChanged(object? sender, HeightUnit e)
    {
        UpdateHeightUnits(e.ToLengthUnit());
    }

    private void SettingsServiceOnMassUnitChanged(object? sender, WeightUnit e)
    {
        UpdateMassUnits(e.ToMassUnit());
    }

    #endregion

    #region Helper Methods

    private void UpdateUnits()
    {
        UpdateDistanceUnits(SettingsService.DistanceUnit.ToLengthUnit());
        UpdateHeightUnits(SettingsService.HeightUnit.ToLengthUnit());
        UpdateMassUnits(SettingsService.MassUnit.ToMassUnit());
    }

    private void UpdateDistanceUnits(LengthUnit unit)
    {
        Distance = Distance.ToUnit(unit);
    }
        
    private void UpdateHeightUnits(LengthUnit unit)
    {
        Height = Height.ToUnit(unit);
    }

    private void UpdateMassUnits(UnitsNet.Units.MassUnit unit)
    {
        Mass = Mass.ToUnit(unit);
    }

    #endregion
}