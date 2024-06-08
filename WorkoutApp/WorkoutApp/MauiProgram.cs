using CommunityToolkit.Maui;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Syncfusion.Maui.Core.Hosting;
using WorkoutApp.Constants;
using WorkoutApp.Core.Factories;
using WorkoutApp.Core.Strategies.BarbellRacking;
using WorkoutApp.DAL.Context;
using WorkoutApp.Services;
using WorkoutApp.ViewModels;
using WorkoutApp.Views;

namespace WorkoutApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "FontAwesomeBrands");
                fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "FontAwesomeRegular");
                fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FontAwesomeSolid");
            })
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionCore()
            .UseSkiaSharp(true)
            .RegisterLogging()
            .RegisterConfiguration()
            .RegisterServices()
            .RegisterViewModels()
            .RegisterViews();

        return builder.Build();
    }

    private static MauiAppBuilder RegisterLogging(this MauiAppBuilder builder)
    {
#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Logging.AddNLog();

        return builder;
    }

    private static MauiAppBuilder RegisterConfiguration(this MauiAppBuilder builder)
    {
        builder.Configuration.AddUserSecrets<App>();
        return builder;
    }

    private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IAppInfo>(AppInfo.Current);
        builder.Services.AddSingleton<IDeviceDisplay>(DeviceDisplay.Current);
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);
        builder.Services.AddSingleton<IFileSystem>(FileSystem.Current);
        builder.Services.AddSingleton<ISettingsService, PreferencesSettingsService>();
        builder.Services.AddSingleton<IDialogService, DialogService>();
        builder.Services.AddSingleton<IDisplayOrientationService, DisplayOrientationService>();
        builder.Services.AddSingleton<IOneRepMaxStrategyFactory, OneRepMaxStrategyFactory>();
        builder.Services.AddSingleton<IBarbellRackingStrategy<BarbellRackingStrategyInput>, MinimumPlateBarbellRackingStrategy>();

        var connectionString = new SqliteConnectionStringBuilder
        {
            Cache = SqliteCacheMode.Private,
            DataSource = Path.Combine(FileSystem.Current.AppDataDirectory, "workout-app.db"),
            DefaultTimeout = 30,
            //ForeignKeys =
            Mode = SqliteOpenMode.ReadWriteCreate,
            //Password =
        }.ToString();

        builder.Services.AddDbContextFactory<WorkoutAppContext>(options =>
        {
            options.UseSqlite(connectionString);
#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
        }, ServiceLifetime.Scoped);

        return builder;
    }

    private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddTransient<OneRepMaxCalculatorPageViewModel>();
        builder.Services.AddTransient<PlateCalculatorPageViewModel>();
        builder.Services.AddSingleton<ProgressPageViewModel>();
        builder.Services.AddSingleton<HistoryPageViewModel>();
        builder.Services.AddSingleton<SettingsPageViewModel>();
        builder.Services.AddSingleton<GeneralSettingsPageViewModel>();
        builder.Services.AddSingleton<AboutPageViewModel>();

        return builder;
    }

    private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<OneRepMaxCalculatorPage>();
        builder.Services.AddTransient<PlateCalculatorPage>();
        builder.Services.AddSingleton<ProgressPage>();
        builder.Services.AddSingleton<HistoryPage>();
        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<GeneralSettingsPage>();
        builder.Services.AddSingleton<AboutPage>();

        Routing.RegisterRoute(RouteKeys.GeneralSettings, typeof(GeneralSettingsPage));
        Routing.RegisterRoute(RouteKeys.PlateCalculator, typeof(PlateCalculatorPage));
        Routing.RegisterRoute(RouteKeys.OneRepMaxCalculator, typeof(OneRepMaxCalculatorPage));
        Routing.RegisterRoute(RouteKeys.About, typeof(AboutPage));

        return builder;
    }
}
