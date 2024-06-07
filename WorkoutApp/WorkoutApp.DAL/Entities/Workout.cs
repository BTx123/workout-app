using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutApp.DAL.Entities;

public class Workout : EntityBase
{
    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column("started_at")]
    public DateTime StartedAt { get; set; }

    [Column("stopped_at")]
    public DateTime StoppedAt { get; set; }

    public ICollection<SetGroup> SetGroups { get; set; }

    [Column("note")]
    [MaxLength(1000)]
    public string Note { get; set; } = string.Empty;
}