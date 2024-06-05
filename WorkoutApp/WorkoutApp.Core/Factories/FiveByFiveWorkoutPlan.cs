using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkoutApp.Core.Models;

namespace WorkoutApp.Core.Factories;

[ObservableObject]
public partial class FiveByFiveWorkoutPlan : ModelBase, IWorkoutPlan
{
    public string Name => "five-by-five-workout-plan";

    [ObservableProperty]
    private ObservableCollection<WorkoutSetup> _workouts = new();
}