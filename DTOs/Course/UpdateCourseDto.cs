using System.ComponentModel.DataAnnotations;

namespace LmsProject.DTOs.Course
{
    public class UpdateCourseDto
    {

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? CourseCode { get; set; }

        public int? UnitsCount { get; set; }

        public string? Semester { get; set; }

        public int? Capacity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }

        public string? ScheduleInfo { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
