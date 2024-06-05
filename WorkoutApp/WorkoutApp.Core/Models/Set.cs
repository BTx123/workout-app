using CommunityToolkit.Mvvm.ComponentModel;
using UnitsNet;

namespace WorkoutApp.Core.Models;

[ObservableObject]
public partial class Set : ModelBase, ISet
{
    [ObservableProperty]
    private DateTime _startedAt;

    [ObservableProperty]
    private DateTime _stoppedAt;

    [ObservableProperty]
    private int _repetitions;

    [ObservableProperty]
    private Mass _weight;

    [ObservableProperty]
    private bool _isAmrap;
}