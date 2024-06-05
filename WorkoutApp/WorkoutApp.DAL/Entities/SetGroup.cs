using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.DAL.Entities;

public class SetGroup : EntityBase
{
    public required Exercise Exercise { get; set; }

    public required Workout Workout { get; set; }

    public ICollection<Set> Sets { get; set; } = new List<Set>();

    [MaxLength(1000)]
    public string Note { get; set; } = string.Empty;
}