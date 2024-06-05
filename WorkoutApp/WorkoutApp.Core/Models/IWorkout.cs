using System.Collections.ObjectModel;
using WorkoutApp.Core.Database;
using WorkoutApp.Core.Library;

namespace WorkoutApp.Core.Models;

public interface IWorkout : IHasId, INamed
{
    DateTime StartedAt { get; set; }

    DateTime StoppedAt { get; set; }

    TimeSpan Duration { get; }

    ObservableCollection<ISetGroup> SetGroups { get; set; }

    string Note { get; set; }
}