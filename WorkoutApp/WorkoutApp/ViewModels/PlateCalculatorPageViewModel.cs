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
        _rackingWeight = 0; // do not use public property here, will trigger event handler
        _rackingWeightUnit = SettingsService.MassType.ToMassUnit();

        SettingsService.MassUnitChanged += SettingsServiceOnMassUnitChanged;
    }

    [ObservableProperty]
    // [Range(0, double.PositiveInfinity)]
    private double? _rackingWeight;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(RackingWeightUnitString))]
    private MassUnit _rackingWeightUnit;

    public string RackingWeightUnitString => Mass.GetAbbreviation(RackingWeightUnit);

    [ObservableProperty]
    private ObservableCollection<PlateCount> _plateCounts = new();

    #region Event Handlers

    private void SettingsServiceOnMassUnitChanged(object? sender, MassType e)
    {
        RackingWeightUnit = e.ToMassUnit();
    }

    partial void OnRackingWeightChanged(double? value)
    {
        try
        {
            if (!value.HasValue)
            {
                PlateCounts.Clear();
                return;
            }

            var rackingResult = _barbellRackingStrategy.Execute(new BarbellRackingStrategyInput
            {
                BarbellWeight = SettingsService.DefaultBarbellWeight,
                AvailablePlates = AppSettings.Default.AvailablePlates,
                DesiredWeight = Mass.From(value.Value, RackingWeightUnit),
                AllowRemainingWeight = true
            });
            if (!rackingResult.IsSuccess)
            {
                DialogService.DisplayAlertAsync("Error", rackingResult.ErrorMessage() ?? string.Empty, "OK");
                return;
            }

            PlateCounts.Clear();
            foreach (var (weight, count) in rackingResult.Value.OrderByDescending(w => w.Key))
            {
                PlateCounts.Add(new PlateCount
                {
                    Weight = weight,
                    Count = count
                });
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Unexpected error occurred");
            DialogService.DisplayAlertAsync("Error", e.Message, "OK");
        }
    }

    #endregion
}