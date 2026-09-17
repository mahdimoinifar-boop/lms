
using System.ComponentModel.DataAnnotations;
using LmsProject.Models.Enums;

namespace LmsProject.DTOs.User
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Fullname { get; set; } = string.Empty;

        public UserRole Role { get; set; }
    }
}

