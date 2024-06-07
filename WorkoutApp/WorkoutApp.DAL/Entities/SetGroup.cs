using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutApp.DAL.Entities;

public class SetGroup : EntityBase
{
    [Column("exercise_id")]
    public int ExerciseId { get; set; }

    public Exercise Exercise { get; set; }

    [Column("workout_id")]
    public int WorkoutId { get; set; }

    public Workout Workout { get; set; }

    public ICollection<Set> Sets { get; set; } = new List<Set>();

    [Column("note")]
    [MaxLength(1000)]
    public string Note { get; set; } = string.Empty;
}