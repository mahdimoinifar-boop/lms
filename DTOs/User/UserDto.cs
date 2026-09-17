using LmsProject.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace LmsProject.DTOs.User
{
    public class UserDto
    {

        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Fullname { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
