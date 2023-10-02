using System.Collections.ObjectModel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using Microsoft.Extensions.Logging;
using WorkoutApp.Services;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using UnitsNet;
using WorkoutApp.Core.Constants;
using WorkoutApp.Core.Database;
using WorkoutApp.Core.Factories;
using WorkoutApp.Core.Library;
using WorkoutApp.Core.Models;
using WorkoutApp.Core.Strategies.OneRepMax;
using User = WorkoutApp.Core.Models.User;

namespace WorkoutApp.ViewModels;

public partial class ProgressPageViewModel : ViewModelBase<ProgressPageViewModel>
{
    private readonly IDbContextFactory<WorkoutAppModel> _dbContextFactory;
    private readonly IOneRepMaxStrategyFactory _oneRepMaxStrategyFactory;

    public ProgressPageViewModel(IDbContextFactory<WorkoutAppModel> dbContextFactory, IOneRepMaxStrategyFactory oneRepMaxStrategyFactory, IDialogService dialogService, ISettingsService settingsService, ILogger<ProgressPageViewModel> logger)
        : base(dialogService, settingsService, logger)
    {
        _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        _oneRepMaxStrategyFactory = oneRepMaxStrategyFactory;
    }

    public ObservableCollection<ISeries> Series { get; set; } = new()
    {
        //new LineSeries<int>
        //{
        //    Name = "Line Series",
        //    Values = new int[] { 4, 6, 5, 3, -3, -1, 2 },
        //    Fill = new SolidColorPaint(SKColors.LightSkyBlue),
        //    Stroke = new SolidColorPaint(SKColors.DodgerBlue) { StrokeThickness = 10 },
        //    GeometrySize = 0
        //},
        //new ColumnSeries<double>
        //{
        //    Values = new double[] { 2, 5, 4, -2, 4, -3, 5 }
        //}
    };

    public Axis[] XAxes { get; set; } = {
        new DateTimeAxis(TimeSpan.FromDays(1), date => date.ToString("MMMM dd"))
    };

    public LabelVisual ChartTitle { get; set; } = new()
    {
        Text = "Big 3",
        TextSize = 25,
        Padding = new LiveChartsCore.Drawing.Padding(15),
        Paint = new SolidColorPaint(SKColors.DarkSlateGray)
    };

    [RelayCommand]
    private async Task Loaded(CancellationToken cancellationToken = default)
    {
        try
        {
            IsBusy = true;

            await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

            var exercises = await db.Exercises.ToListAsync(cancellationToken: cancellationToken);

            Series.Clear();

            foreach (var exercise in exercises)
            {
                var points = await GetOneRepMaxesForExerciseAsync(exercise, end: DateTime.UtcNow,
                    cancellationToken: cancellationToken);
                Series.Add(new LineSeries<DateTimePoint>
                {
                    Name = exercise.Name,
                    Values = points.Select(p => new DateTimePoint(p.X, p.Y?.ToUnit(SettingsService.MassUnit.ToMassUnit()).Value)),
                });
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Failed to load data");
        }
        finally
        {
            IsBusy = false;
        }
    }

    #region Helper Methods

    private async Task<IEnumerable<Point2D<DateTime, Mass?>>> GetOneRepMaxesForExerciseAsync(Exercise exercise, DateTime? start = null, DateTime? end = null, CancellationToken cancellationToken = default)
    {
        start ??= DateTime.MinValue;
        end ??= DateTime.UtcNow;

        await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var user = await db.Users.FirstOrDefaultAsync(user => user.Username == User.DefaultUser, cancellationToken);
        if (user == null)
        {
            Logger.LogError("Default user not found");
            return new List<Point2D<DateTime, Mass?>>();
        }

        var strategy = SettingsService.OneRepMaxStrategy;
        var factoryResult = _oneRepMaxStrategyFactory.Create(strategy);
        if (!factoryResult.IsSuccess)
        {
            Logger.LogError("Failed to load data: {Message}", factoryResult.ErrorMessage());
            return new List<Point2D<DateTime, Mass?>>();
        }

        var oneRepMaxStrategy = factoryResult.Value;
        var data = await db.Sets
            .Where(s => s.SetGroup.Workout.User == user)
            .Where(s => s.SetGroup.Exercise == exercise)
            .Where(s => s.SetGroup.Workout.CompletedAt.HasValue)
            .Where(s => s.SetGroup.Workout.CompletedAt >= start.Value.Date)
            .Where(s => s.SetGroup.Workout.CompletedAt <= end.Value.Date)
            .GroupBy(s => s.SetGroup.Workout.CompletedAt!.Value.Date)
            .ToListAsync(cancellationToken: cancellationToken);

        var points = data
            .Select(grouping => new
            {
                grouping,
                oneRepMaxes = grouping.Select(set =>
                {
                    var oneRepMaxResult = oneRepMaxStrategy.Execute(new OneRepMaxStrategyInput
                    {
                        Repetitions = set.Repetitions,
                        Weight = Mass.FromKilograms(set.WeightKg)
                    });
                    return oneRepMaxResult.IsSuccess ? oneRepMaxResult.Value : Mass.Zero;
                })
            })
            .Select(t => new Point2D<DateTime, Mass?>(t.grouping.Key, t.oneRepMaxes.Any() ? t.oneRepMaxes.Max() : null))
            .ToList();

        return points;
    }

    #endregion
}

public record Point2D<TX, TY>(TX X, TY Y);
