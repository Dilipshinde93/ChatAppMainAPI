using System.ComponentModel.DataAnnotations;

namespace FriendsAPI.Models
{
    public class AppUser
    {
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string? FullName { get; set; }

        public string? ProfileImageUrl { get; set; }
    }
}
