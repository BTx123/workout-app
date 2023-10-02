using UnitsNet;

namespace WorkoutApp.Core.Models;

public interface IBarbell
{
    public string Name { get; set; }

    public Mass Weight { get; set; }
}