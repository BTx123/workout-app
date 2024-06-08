using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutApp.DAL.Entities;

public class Set : EntityBase
{
    [Column("set_group_id")]
    public int SetGroupId { get; set; }

    public SetGroup SetGroup { get; set; }

    [Column("started_at")]
    public DateTime StartedAt { get; set; }

    [Column("stopped_at")]
    public DateTime StoppedAt { get; set; }

    [Column("repetitions")]
    [Range(0, double.PositiveInfinity)]
    public int Repetitions { get; set; }

    [Column("mass_kg")]
    [Range(0, double.PositiveInfinity)]
    public decimal MassKg { get; set; }

    [Column("is_amrap")]
    public bool IsAmrap { get; set; }
}