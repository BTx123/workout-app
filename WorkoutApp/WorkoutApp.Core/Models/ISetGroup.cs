using System.Collections.ObjectModel;
using WorkoutApp.Core.Database;

namespace WorkoutApp.Core.Models;

public interface ISetGroup : IHasId
{
    string Note { get; set; }

    Exercise Exercise { get; set; }

    ObservableCollection<Set> Sets { get; }
}