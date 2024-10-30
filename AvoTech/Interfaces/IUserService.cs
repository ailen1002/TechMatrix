using System.Threading.Tasks;
using AvoTech.Models;

namespace AvoTech.Interfaces;

public interface IUserService
{
    Task<bool> UserExistsAsync(string username);
    Task AddUserAsync(User user);
}