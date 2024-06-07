using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkoutApp.DAL.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Exercise : EntityBase
{
    [Column("name")]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Column("barbell_id")]
    public int? BarbellId { get; set; }

    public Barbell? Barbell { get; set; }
}
