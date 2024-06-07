using CommunityToolkit.Mvvm.ComponentModel;
using UnitsNet;

namespace WorkoutApp.Core.Models;

public partial class PlateCount : ObservableObject
{
    [ObservableProperty]
    private Mass _weight;

    [ObservableProperty]
    private int _count;
}