using System;
using System.Threading.Tasks;
using AvoTech.Data;
using AvoTech.Interfaces;
using AvoTech.Models;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace AvoTech.Services;

public class UserService(MainDbContext context) : IUserService
{
    private readonly MainDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    public async Task<bool> UserExistsAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username cannot be null or whitespace.", nameof(username));
        }
        
        return await _context.Users.AnyAsync(u => u.UserName == username).ConfigureAwait(false);
    }

    public async Task AddUserAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        try
        {
            await _context.Users.AddAsync(user).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            Logger.Info("User added successfully.");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error adding user.");
            throw; // Re-throwing the exception to ensure it can be handled by the caller
        }
    }
}