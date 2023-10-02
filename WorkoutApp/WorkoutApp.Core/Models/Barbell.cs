using System.ComponentModel;
using UnitsNet.Units;
using UnitsNet;
using WorkoutApp.Core.Constants;

namespace WorkoutApp.Core.Models;

public partial class Barbell : Database.Barbell, IBarbell
{
    public static readonly Barbell StandardBarbell = Create("Standard Barbell", Mass.FromPounds(45).ToUnit(MassUnit.Kilogram).Value);

    public static readonly Barbell OlympicBarbell = Create("Olympic Barbell", Mass.FromKilograms(20).Value);

    public static readonly Barbell AlternativeOlympicBarbell = Create("Light Olympic Barbell", Mass.FromKilograms(15).Value);

    public static readonly Barbell SafetySquatBar = Create("Safety Squat Bar", Mass.FromPounds(70).ToUnit(MassUnit.Kilogram).Value);

    public static readonly Barbell EzCurlBar = Create("EZ Curl Bar", Mass.FromPounds(25).ToUnit(MassUnit.Kilogram).Value);

    public Mass Weight
    {
        get => Mass.From(WeightKg, MassUnit.Kilogram);
        set
        {
            var mass = value.ToUnit(MassUnit.Kilogram);
            if (Weight.Equals(mass, Unit.MassTolerance)) return;
            WeightKg = mass.Value;
        }
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(WeightKg):
                OnPropertyChanged(nameof(Weight));
                break;
        }
        base.OnPropertyChanged(e);
    }
}
