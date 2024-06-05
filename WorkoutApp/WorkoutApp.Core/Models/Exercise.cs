using CommunityToolkit.Mvvm.ComponentModel;

namespace WorkoutApp.Core.Models;

[ObservableObject]
public partial class Exercise : ModelBase, IExercise
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private Barbell? _barbell;
}