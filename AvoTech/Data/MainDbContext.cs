using System;
using AvoTech.Models;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace AvoTech.Data;

public class MainDbContext : DbContext
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    public DbSet<User> Users { get; set; }
    
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
        EnsureDatabaseCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.UserName)
            .HasMaxLength(64);
    }
    
    // 确保数据库和表已创建
    public void EnsureDatabaseCreated()
    {
        try
        {
            var isCreated = Database.EnsureCreated();
            Logger.Info(isCreated ? "Database created." : "Database already exists.");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error ensuring database creation.");
        }
    }
}