using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvoTech.Configs;
using AvoTech.Data;
using AvoTech.Interfaces;
using AvoTech.Services;
using AvoTech.ViewModels;
using AvoTech.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using SQLitePCL;

namespace AvoTech;

public class App : Application
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    private readonly IHost _host;
    
    public App()
    {
        try
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .Build();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error during host initialization.");
            throw;
        }
    }
    
    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        var dbPath = AppSettings.GetDatabasePath();

        // Register MainDbContext with SQLite database path
        services.AddDbContext<MainDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));
    
        // Register services
        services.AddTransient<IUserService, UserService>();

        // Register ViewModels
        services.AddTransient<LoginViewModel>();
        services.AddTransient<RegisterViewModel>();

        // Register Views
        services.AddTransient<LoginView>();
        services.AddTransient<RegisterView>();
    }
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void InitializeMainView()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var loginView  = _host.Services.GetRequiredService<LoginView>();
            desktop.MainWindow = loginView;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            var loginView  = _host.Services.GetRequiredService<LoginView>();
            singleViewPlatform.MainView = loginView;
        }
    }
    
    public override void OnFrameworkInitializationCompleted()
    {
        InitializeMainView();
        base.OnFrameworkInitializationCompleted();
    }
}