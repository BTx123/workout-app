using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WorkoutApp.DAL.Entities;

[Index(nameof(MassKg), IsUnique = true)]
public class Plate : EntityBase
{
    [Range(0, double.PositiveInfinity)]
    public decimal MassKg { get; set; }
}
