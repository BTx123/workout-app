using Microsoft.Extensions.Logging;
using UnitsNet;
using UnitsNet.Units;
using WorkoutApp.Core.Factories;
using WorkoutApp.Core.Library;
using WorkoutApp.Core.Strategies.OneRepMax;
using WorkoutApp.Services;
using WorkoutApp.Core.Extensions;
using WorkoutApp.DAL.Constants;

namespace WorkoutApp.ViewModels;

public partial class OneRepMaxCalculatorPageViewModel : ViewModelBase<OneRepMaxCalculatorPageViewModel>
{
    private readonly IOneRepMaxStrategyFactory _oneRepMaxStrategyFactory;

    public OneRepMaxCalculatorPageViewModel(IOneRepMaxStrategyFactory oneRepMaxStrategyFactory, IDialogService dialogService,
        ISettingsService settingsService, ILogger<OneRepMaxCalculatorPageViewModel> logger)
        : base(dialogService, settingsService, logger)
    {
        _oneRepMaxStrategyFactory = oneRepMaxStrategyFactory ??
                                    throw new ArgumentNullException(nameof(oneRepMaxStrategyFactory));

        Title = "1RM Calculator";
        _strategy = SettingsService.OneRepMaxStrategy; // do not use public property here, will trigger event handler
        _repetitions = 0;
        _weightLifted = 0;
        _weightLiftedUnit = SettingsService.MassType.ToMassUnit();

        SettingsService.MassUnitChanged += SettingsServiceOnMassUnitChanged;
        SettingsService.OneRepMaxStrategyChanged += SettingsServiceOnOneRepMaxStrategyChanged;
    }

    #region Properties

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(OneRepMax))]
    private OneRepMaxStrategy _strategy;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(OneRepMax))]
    private int? _repetitions;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(OneRepMax))]
    private double? _weightLifted;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(WeightLiftedUnitString))]
    [NotifyPropertyChangedFor(nameof(OneRepMax))]
    private MassUnit _weightLiftedUnit;

    public string WeightLiftedUnitString => Mass.GetAbbreviation(WeightLiftedUnit);

    public Mass? OneRepMax
    {
        get
        {
            var strategyResult = _oneRepMaxStrategyFactory.Create(Strategy);
            if (!strategyResult.IsSuccess)
            {
                Logger.LogError("Failed to get one rep max strategy: {Message}", strategyResult.ErrorMessage());
                return null;
            }

            if (!Repetitions.HasValue) return null;
            if (!WeightLifted.HasValue) return null;

            var strategy = strategyResult.Value;
            var calculationResult = strategy.Execute(new OneRepMaxStrategyInput
            {
                Repetitions = Repetitions.Value,
                Weight = Mass.From(WeightLifted.Value, WeightLiftedUnit)
            });
            if (!calculationResult.IsSuccess)
            {
                Logger.LogError("Failed to calculate one rep max strategy: {Message}", calculationResult.ErrorMessage());
                return null;
            }

            return calculationResult.Value;
        }
    }

    #endregion

    #region Event Handlers

    private void SettingsServiceOnMassUnitChanged(object? sender, MassType e)
    {
        WeightLiftedUnit = e.ToMassUnit();
    }

    private void SettingsServiceOnOneRepMaxStrategyChanged(object? sender, OneRepMaxStrategy e)
    {
        Strategy = e;
    }

    partial void OnWeightLiftedUnitChanged(MassUnit oldValue, MassUnit newValue)
    {
        if (!WeightLifted.HasValue) return;

        var oldMass = Mass.From(WeightLifted.Value, oldValue);
        var newMass = oldMass.ToUnit(newValue);
        WeightLifted = newMass.Value;
    }

    #endregion
}