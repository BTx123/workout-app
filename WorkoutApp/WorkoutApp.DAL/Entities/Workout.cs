using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.DAL.Entities;

public class Workout : EntityBase
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public DateTime StartedAt { get; set; }

    public DateTime StoppedAt { get; set; }

    public ICollection<SetGroup> SetGroups { get; set; } = new List<SetGroup>();

    [MaxLength(1000)]
    public string Note { get; set; } = string.Empty;
}