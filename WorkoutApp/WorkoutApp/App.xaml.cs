using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkoutApp.DAL.Constants;
using WorkoutApp.DAL.Context;
using WorkoutApp.Services;

namespace WorkoutApp;

public partial class App
{
    public App(
        IDbContextFactory<WorkoutAppContext> dbContextFactory,
        ISettingsService settingsService,
        IDeviceDisplay deviceDisplay,
        IDisplayOrientationService displayOrientationService,
        ILogger<AppShell> logger)
    {
        InitializeComponent();

        if (settingsService == null) throw new ArgumentNullException(nameof(settingsService));

        settingsService.ThemeChanged += SettingsServiceOnThemeChanged;
        settingsService.ThemeColorChanged += SettingsServiceOnThemeColorChanged;

        LiveCharts.Configure(config =>
                config
                    // registers SkiaSharp as the library backend
                    // REQUIRED unless you build your own
                    .AddSkiaSharp()

                    // adds the default supported types
                    // OPTIONAL but highly recommend
                    .AddDefaultMappers()

                    // select a theme, default is Light
                    // OPTIONAL
                    .AddDarkTheme()
                    //.AddLightTheme()

                    // finally register your own mappers
                    // you can learn more about mappers at:
                    //.HasMap<City>((city, point) =>
                    //{
                    //    point.PrimaryValue = city.Population;
                    //    point.SecondaryValue = point.Context.Index;
                    //})
            // .HasMap<Foo>( .... )
            // .HasMap<Bar>( .... )
        );

        var theme = settingsService.Theme;
        LoadTheme(theme);

        var themeColor = settingsService.ThemeColor;
        LoadThemeColor(themeColor);

        MainPage = new AppShell(dbContextFactory, settingsService, deviceDisplay, displayOrientationService, logger);
    }

    #region Event Handlers

    private static void SettingsServiceOnThemeChanged(object? sender, Theme e)
    {
        LoadTheme(e);
    }

    private void SettingsServiceOnThemeColorChanged(object? sender, Color e)
    {
        LoadThemeColor(e);
    }

    #endregion

    #region Helpers

    private static void LoadTheme(Theme theme)
    {
        if (Current == null) return;

        Current.UserAppTheme = theme switch
        {
            Theme.Light => AppTheme.Light,
            Theme.Dark => AppTheme.Dark,
            Theme.System => AppTheme.Unspecified,
            _ => AppTheme.Unspecified
        };
    }

    private static void LoadThemeColor(Color color)
    {
        if (Current == null) return;

        var colorResources = Current.Resources.MergedDictionaries.First();
        colorResources["Primary"] = color;
        colorResources["Secondary"] = color.WithSaturation(color.GetSaturation() * 0.5f).WithLuminosity(0.5f * (color.GetLuminosity() + 1));
        colorResources["Tertiary"] = color.WithSaturation(0.5f * (color.GetSaturation() + 1)).WithLuminosity(color.GetLuminosity() * 0.5f);
    }

    #endregion
}
