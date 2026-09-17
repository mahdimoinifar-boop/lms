using System.ComponentModel.DataAnnotations;

namespace LmsProject.DTOs.User
{
    public class UpdateUserDto
    {
        [Required]
        [MaxLength(100)]
        public string Fullname { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
