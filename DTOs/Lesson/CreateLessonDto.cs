using System.ComponentModel.DataAnnotations;

namespace LmsProject.DTOs.Lesson
{
    public class CreateLessonDto
    {

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }

        public string? VideoUrl { get; set; }

        public string? FileUrl { get; set; }

        public string? OnlineClassUrl { get; set; }

        [Range(1, int.MaxValue)]
        public int Order { get; set; }
    }
}
