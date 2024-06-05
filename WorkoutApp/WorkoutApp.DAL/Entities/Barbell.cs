using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.DAL.Entities;

public class Barbell : EntityBase
{
    [MaxLength(100)]
    public required string Name { get; set; }

    [Range(0, double.PositiveInfinity)]
    public decimal MassKg { get; set; }
}
