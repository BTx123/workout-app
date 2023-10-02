using WorkoutApp.Core.Library;
using WorkoutApp.Core.Models;

namespace WorkoutApp.Core.Factories;

public interface IWorkoutPlanFactory<in TInput, out TOutput> : IFactory<TInput, TOutput>
    where TInput : IWorkoutFactoryInput
    where TOutput : IWorkoutPlan
{
}