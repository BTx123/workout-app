using UnitsNet;
using WorkoutApp.Core.Database;
using WorkoutApp.Core.Library;

namespace WorkoutApp.Core.Models;

public interface IBarbell : IHasId, INamed
{
    public Mass Weight { get; set; }
}