using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using UnitsNet;
using UnitsNet.Units;
using WorkoutApp.Core.Extensions;
using WorkoutApp.Core.Library;
using WorkoutApp.Core.Models;
using WorkoutApp.Core.Models.Settings;
using WorkoutApp.Core.Strategies.BarbellRacking;
using WorkoutApp.DAL.Constants;
using WorkoutApp.Services;

namespace WorkoutApp.ViewModels;

public partial class PlateCalculatorPageViewModel : ViewModelBase<PlateCalculatorPageViewModel>
{
    private readonly IBarbellRackingStrategy<BarbellRackingStrategyInput> _barbellRackingStrategy;

    public PlateCalculatorPageViewModel(
        IBarbellRackingStrategy<BarbellRackingStrategyInput> barbellRackingStrategy,
        IDialogService dialogService, ISettingsService settingsService, ILogger<PlateCalculatorPageViewModel> logger)
        : base(dialogService, settingsService, logger)
    {
        _barbellRackingStrategy = barbellRackingStrategy ?? throw new ArgumentNullException(nameof(barbellRackingStrategy));

        Title = "Plate Calculator";
        _rackingWeightUnit = SettingsService.MassType.ToMassUnit(); // do not use public property here, will trigger event handler
        _rackingWeight = Mass.From(0, _rackingWeightUnit);

        SettingsService.MassUnitChanged += SettingsServiceOnMassUnitChanged;
    }

    [ObservableProperty]
    // [Range(0, double.PositiveInfinity)]
    private Mass? _rackingWeight;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(RackingWeightUnitString))]
    private MassUnit _rackingWeightUnit;

    public string RackingWeightUnitString => Mass.GetAbbreviation(RackingWeightUnit);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PlateCounts))]
    private bool _showPlatesPerSide = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(RemainingWeight))]
    private ObservableCollection<PlateCount> _plateCounts = new();

    public Mass RemainingWeight => Mass.From(PlateCounts.Sum(c => c.Count * c.Weight.Kilograms), RackingWeightUnit);

    #region Event Handlers

    private void SettingsServiceOnMassUnitChanged(object? sender, MassType e)
    {
        RackingWeightUnit = e.ToMassUnit();
    }

    partial void OnRackingWeightChanged(Mass? value)
    {
        CalculatePlateCount(value, ShowPlatesPerSide);
    }

    partial void OnShowPlatesPerSideChanged(bool value)
    {
        CalculatePlateCount(RackingWeight, value);
    }

    #endregion

    #region Helper Methods

    private void CalculatePlateCount(Mass? rackingWeight, bool computePlatesPerSide)
    {
        try
        {
            if (!rackingWeight.HasValue)
            {
                PlateCounts.Clear();
                return;
            }

            var rackingResult = _barbellRackingStrategy.Execute(new BarbellRackingStrategyInput
            {
                BarbellWeight = SettingsService.DefaultBarbellWeight,
                AvailablePlates = AppSettings.Default.AvailablePlates,
                DesiredWeight = rackingWeight.Value,
                AllowRemainingWeight = true
            });
            if (!rackingResult.IsSuccess)
            {
                PlateCounts.Clear();
                Logger.LogWarning("Failed to determine barbell racking: {Message}", rackingResult.ErrorMessage() ?? string.Empty);
                // DialogService.DisplayAlertAsync("Error", rackingResult.ErrorMessage() ?? string.Empty, "OK");
                return;
            }

            PlateCounts.Clear();
            foreach (var (weight, count) in rackingResult.Value.OrderByDescending(w => w.Key))
            {
                PlateCounts.Add(new PlateCount
                {
                    Weight = weight,
                    Count = computePlatesPerSide ? count / 2 : count
                });
            }
            OnPropertyChanged(nameof(RemainingWeight));
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Unexpected error occurred");
            DialogService.DisplayAlertAsync("Error", e.Message, "OK");
        }
    }

    #endregion
}