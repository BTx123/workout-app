using UnitsNet;

namespace WorkoutApp.Core.Models;

public interface ISet
{
    int Index { get; set; }

    bool IsDone { get; set; }

    int Repetitions { get; set; }

    Mass Weight { get; set; }

    bool IsAmrap { get; set; }
}