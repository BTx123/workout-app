using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.DAL.Entities;

public class Plate : EntityBase
{
    [Range(0, double.PositiveInfinity)]
    public decimal MassKg { get; set; }
}
