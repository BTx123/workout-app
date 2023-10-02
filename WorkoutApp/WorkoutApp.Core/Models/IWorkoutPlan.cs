using WorkoutApp.Core.Factories;
using WorkoutApp.Core.Library;

namespace WorkoutApp.Core.Models;

public interface IWorkoutPlan : INamed
{
    IEnumerable<WorkoutSetup> Workouts { get; }
}