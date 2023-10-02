using Microsoft.Extensions.Logging;
using UnitsNet;
using WorkoutApp.Core.Constants;
using WorkoutApp.Core.Factories;
using WorkoutApp.Core.Library;
using WorkoutApp.Core.Strategies.OneRepMax;
using WorkoutApp.Services;
using MassUnit = UnitsNet.Units.MassUnit;

namespace WorkoutApp.ViewModels
{
    public partial class OneRepMaxCalculatorViewModel : ViewModelBase<OneRepMaxCalculatorViewModel>
    {
        private readonly IOneRepMaxStrategyFactory _oneRepMaxStrategyFactory;

        public OneRepMaxCalculatorViewModel(IOneRepMaxStrategyFactory oneRepMaxStrategyFactory, IDialogService dialogService,
            ISettingsService settingsService, ILogger<OneRepMaxCalculatorViewModel> logger)
            : base(dialogService, settingsService, logger)
        {
            _oneRepMaxStrategyFactory = oneRepMaxStrategyFactory ??
                                        throw new ArgumentNullException(nameof(oneRepMaxStrategyFactory));

            SettingsService.MassUnitChanged += SettingsServiceOnMassUnitChanged;
            SettingsService.OneRepMaxStrategyChanged += SettingsServiceOnOneRepMaxStrategyChanged;

            _strategy = SettingsService.OneRepMaxStrategy;
            _repetitions = 0;
            _weightLifted = 0;
            _weightLiftedUnit = SettingsService.MassUnit.ToMassUnit();
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

        private void SettingsServiceOnMassUnitChanged(object? sender, Core.Constants.WeightUnit e)
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
}
