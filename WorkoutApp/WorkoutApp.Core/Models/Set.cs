using CommunityToolkit.Mvvm.ComponentModel;
using UnitsNet.Units;
using UnitsNet;

namespace WorkoutApp.Core.Models;

public partial class Set : ObservableObject, ISet
{
    private readonly Database.Set _set;

    private Set(Database.Set set)
    {
        _set = set ?? throw new ArgumentNullException(nameof(set));

        _index = _set.Index;
        _isDone = _set.IsDone;
        _repetitions = _set.Repetitions;
        _weight = Mass.FromKilograms(_set.WeightKg);
        _isAmrap = _set.IsAmrap;
    }

    public static Set From(Database.Set set)
    {
        return new Set(set);
    }

    //public Mass Weight
    //{
    //    get => Mass.FromKilograms(_set.WeightKg);
    //    set
    //    {
    //        var mass = value.ToUnit(MassUnit.Kilogram);
    //        if (Weight.Equals(mass, Unit.MassTolerance)) return;
    //        OnPropertyChanged();
    //        _set.WeightKg = mass.Value;
    //        OnPropertyChanged();
    //    }
    //}

    [ObservableProperty]
    private int _index;

    partial void OnIndexChanged(int value)
    {
        _set.Index = value;
    }

    //public int Index { get => _set.Index; set => SetProperty(ref _set.Index, value); }

    [ObservableProperty]
    private bool _isDone;

    partial void OnIsDoneChanged(bool value)
    {
        _set.IsDone = value;
    }

    [ObservableProperty]
    private int _repetitions;

    partial void OnRepetitionsChanged(int value)
    {
        _set.Repetitions = value;
    }

    [ObservableProperty]
    private Mass _weight;

    partial void OnWeightChanged(Mass value)
    {
        _set.WeightKg = value.ToUnit(MassUnit.Kilogram).Value;
    }

    [ObservableProperty]
    private bool _isAmrap;

    partial void OnIsAmrapChanged(bool value)
    {
        _set.IsAmrap = value;
    }
}