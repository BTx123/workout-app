using System.ComponentModel.DataAnnotations.Schema;
using CommunityToolkit.Mvvm.ComponentModel;

// ReSharper disable once CheckNamespace
namespace WorkoutApp.Core.Database;

public partial class User : ObservableObject
{
    [NotMapped]
    public static readonly string DefaultUser = "default-user";
}