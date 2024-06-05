using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.DAL.Entities;

public class Set : EntityBase
{
    public required SetGroup SetGroup { get; init; }

    public DateTime StartedAt { get; init; }

    public DateTime StoppedAt { get; init; }

    [Range(0, double.PositiveInfinity)]
    public int Repetitions { get; init; }

    [Range(0, double.PositiveInfinity)]
    public decimal MassKg { get; init; }

    public bool IsAmrap { get; init; }
}