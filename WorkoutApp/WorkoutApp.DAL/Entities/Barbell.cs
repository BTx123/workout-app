using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkoutApp.DAL.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Barbell : EntityBase
{
    [Column("name")]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Column("mass_kg")]
    [Range(0, double.PositiveInfinity)]
    [DefaultValue(0)]
    public decimal MassKg { get; set; }

    public ICollection<Exercise>? Exercises { get; set; }
}
