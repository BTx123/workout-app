using WorkoutApp.Core.Factories;

namespace WorkoutApp.Core.Models;

public class FiveThreeOneWorkoutPlan : IWorkoutPlan
{
    public string Name => "five-three-one-workout-plan";

    public IEnumerable<WorkoutSetup> Workouts { get; init; } = Enumerable.Empty<WorkoutSetup>();
}