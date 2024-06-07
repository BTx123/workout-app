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
    private readonly IAppInfo _appInfo;
    private readonly ILogger<App> _logger;

    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _appInfo = serviceProvider.GetRequiredService<IAppInfo>();
        _logger = serviceProvider.GetRequiredService<ILogger<App>>();

        var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<WorkoutAppContext>>();
        var settingsService = serviceProvider.GetRequiredService<ISettingsService>();
        var deviceDisplay = serviceProvider.GetRequiredService<IDeviceDisplay>();
        var displayOrientationService = serviceProvider.GetRequiredService<IDisplayOrientationService>();
        var logger = serviceProvider.GetRequiredService<ILogger<AppShell>>();

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

        settingsService.ThemeChanged += SettingsServiceOnThemeChanged;
        settingsService.ThemeColorChanged += SettingsServiceOnThemeColorChanged;

        MainPage = new AppShell(dbContextFactory, settingsService, deviceDisplay, displayOrientationService, logger);
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

        SetWindowTitle(window);

        window.Created += (sender, args) =>
        {
            _logger.LogInformation("Started application {ApplicationName} {Version}", _appInfo.Name, _appInfo.VersionString);
        };

        window.Destroying += (sender, args) =>
        {
            _logger.LogInformation("Stopped application {@ApplicationName} {Version}", _appInfo.Name, _appInfo.VersionString);
        };

        return window;
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

    private void SetWindowTitle(Window window)
    {
        var title = $"{_appInfo.Name} v{_appInfo.Version}";
#if DEBUG
            title = $"{title} [DEBUG MODE]";
#endif
            window.Title = title;
    }

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
