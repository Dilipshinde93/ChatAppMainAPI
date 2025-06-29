using FriendsAPI.Dtos;
using FriendsAPI.Models;

namespace FriendsAPI.Services
{
    public interface IUserService
    {
        Task<AppUser?> AuthenticateUserAsync(string username, string password);
    }
}
