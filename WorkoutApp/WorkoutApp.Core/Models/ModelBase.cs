using System.ComponentModel.DataAnnotations;
using WorkoutApp.Core.Database;

namespace WorkoutApp.Core.Models;

public abstract class ModelBase : IHasId
{
    [MaxLength(100)]
    public int? Id { get; init; }
}