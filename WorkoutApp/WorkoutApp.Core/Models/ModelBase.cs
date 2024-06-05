using System.ComponentModel.DataAnnotations;
using WorkoutApp.Core.Database;

namespace WorkoutApp.Core.Models;

public abstract class ModelBase : IHasId
{
    [MaxLength(100)]
    public string? Id { get; init; }
}