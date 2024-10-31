using System;
using System.IO;

namespace AvoTech.Configs;

public static class AppSettings
{
    public static string GetDatabasePath()
    {
        var appDirectory = AppContext.BaseDirectory;
        var appDataDir = Path.Combine(appDirectory, "AvoTech");
        if (!Directory.Exists(appDataDir))
        {
            Directory.CreateDirectory(appDataDir);
        }
        return Path.Combine(appDataDir, "TechMatrix.db");
    }
}