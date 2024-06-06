using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.DAL.Entities;

public class SetGroup : EntityBase
{
    public string ExerciseId { get; set; }

    public Exercise Exercise { get; set; }

    public string WorkoutId { get; set; }

    public Workout Workout { get; set; }

    public ICollection<Set> Sets { get; set; } = new List<Set>();

    [MaxLength(1000)]
    public string Note { get; set; } = string.Empty;
}