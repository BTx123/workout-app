using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutApp.DAL.Entities;

public class Set : EntityBase
{
    [Column("set_group_id")]
    public int SetGroupId { get; init; }

    public SetGroup SetGroup { get; init; }

    [Column("started_at")]
    public DateTime StartedAt { get; init; }

    [Column("stopped_at")]
    public DateTime StoppedAt { get; init; }

    [Column("repetitions")]
    [Range(0, double.PositiveInfinity)]
    public int Repetitions { get; init; }

    [Column("mass_kg")]
    [Range(0, double.PositiveInfinity)]
    public decimal MassKg { get; init; }

    [Column("is_amrap")]
    public bool IsAmrap { get; init; }
}