using WorkoutApp.Core.Database;
using WorkoutApp.Core.Library;

namespace WorkoutApp.Core.Models;

public interface IExercise : IHasId, INamed
{
    public Barbell? Barbell { get; set; }
}