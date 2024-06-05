using System.Collections.ObjectModel;
using WorkoutApp.Core.Database;
using WorkoutApp.Core.Factories;
using WorkoutApp.Core.Library;

namespace WorkoutApp.Core.Models;

public interface IWorkoutPlan : INamed, IHasId
{
    ObservableCollection<WorkoutSetup> Workouts { get; set;  }
}