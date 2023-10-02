namespace WorkoutApp.Core.Models;

public interface ISetGroup
{
    string Note { get; set; }

    Exercise Exercise { get; set; }

    ICollection<Set> Sets { get; }
}