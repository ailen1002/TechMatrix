using System;
using Avalonia;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace AvoTech.Desktop;

sealed class Program
{
    // 创建一个静态的 NLog logger 实例
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        // 初始化 NLog 配置
        using var loggerFactory = InitLogging();
        
        try
        {
            Logger.Info("Application starting...");
                
            // 启动 Avalonia 应用
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            // 捕捉启动期间的异常
            Logger.Error(ex, "Application terminated unexpectedly");
            throw;
        }
        finally
        {
            // 确保在程序结束时释放 NLog 资源
            LogManager.Shutdown();
        }
    }

    // 初始化 NLog 和 LoggerFactory
    private static ILoggerFactory InitLogging()
    {
        try
        {
            Logger.Info("Initializing logging...");

            // 获取 NLog.config 的绝对路径
            const string nlogConfigPath = @"D:\works\MFCProject\RiderProjects\TechMatrix\AvoTech\NLog.config.xml";
            
            // 配置 NLog，指定 config 文件路径
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                // 加载指定路径的 NLog 配置文件
                builder.AddNLog(nlogConfigPath);
            });

            return loggerFactory;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"NLog initialization failed: {ex.Message}");
            throw;
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}