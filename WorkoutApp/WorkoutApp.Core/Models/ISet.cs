using UnitsNet;
using WorkoutApp.Core.Database;

namespace WorkoutApp.Core.Models;

public interface ISet : IHasId
{
    DateTime StartedAt { get; set; }

    DateTime StoppedAt { get; set; }

    int Repetitions { get; set; }

    Mass Weight { get; set; }

    bool IsAmrap { get; set; }
}