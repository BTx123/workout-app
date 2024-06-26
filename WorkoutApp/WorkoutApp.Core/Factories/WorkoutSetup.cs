﻿using WorkoutApp.Core.Library;

namespace WorkoutApp.Core.Factories;

public class WorkoutSetup : INamed
{
    public string Name { get; init; } = string.Empty;

    public DateTime ScheduledDate { get; init; }

    //public IEnumerable<WorkoutExercise> Exercises { get; init; } = Enumerable.Empty<WorkoutExercise>();
}