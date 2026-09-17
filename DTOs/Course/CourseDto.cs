namespace LmsProject.DTOs.Course
{
    public class CourseDto
    {

        public int CourseId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? CourseCode { get; set; }

        public int? UnitsCount { get; set; }

        public string? Semester { get; set; }

        public int? Capacity { get; set; }

        public decimal? Price { get; set; }

        public string? ScheduleInfo { get; set; }

        public int TeacherId { get; set; }

        public string TeacherName { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
