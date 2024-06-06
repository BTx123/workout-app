using System.ComponentModel.DataAnnotations;

namespace WorkoutApp.DAL.Entities;

public class Exercise : EntityBase
{
    [MaxLength(100)]
    public required string Name { get; set; }

    public string? BarbellId { get; set; }

    public Barbell? Barbell { get; set; }
}
