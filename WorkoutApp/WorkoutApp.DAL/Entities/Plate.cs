using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkoutApp.DAL.Entities;

[Index(nameof(MassKg), IsUnique = true)]
public class Plate : EntityBase
{
    [Column("mass_kg")]
    [Range(0, double.PositiveInfinity)]
    public decimal MassKg { get; set; }
}
