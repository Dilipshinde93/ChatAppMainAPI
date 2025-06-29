using FriendsAPI.Dtos;
using FriendsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FriendsAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<AppUser?> AuthenticateUserAsync(string username, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == username && u.Password == password);
        }

    }

}
