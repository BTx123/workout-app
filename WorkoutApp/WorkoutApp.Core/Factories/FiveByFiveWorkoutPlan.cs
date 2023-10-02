using WorkoutApp.Core.Models;

namespace WorkoutApp.Core.Factories;

public class FiveByFiveWorkoutPlan : IWorkoutPlan
{
    public string Name => "five-by-five-workout-plan";

    public IEnumerable<WorkoutSetup> Workouts { get; } = new List<WorkoutSetup>();
}