using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkoutApp.Core.Factories;

namespace WorkoutApp.Core.Models;

[ObservableObject]
public partial class FiveThreeOneWorkoutPlan : ModelBase, IWorkoutPlan
{
    [ObservableProperty]
    private string _name = "five-three-one-workout-plan";

    [ObservableProperty]
    private ObservableCollection<WorkoutSetup> _workouts = new();
}