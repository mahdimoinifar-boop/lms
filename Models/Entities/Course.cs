namespace LmsProject.Models.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        // university course information

        public string? CourseCode { get; set; }
        public int? UnitsCount { get; set; }
        public string? semester { get; set; }

        public int? Capacity { get; set; }

        // Free / Educational Course

        public decimal? Price { get; set; }


        // Class Schedule

        public string? ScheduleInfo { get; set; }


        // Teacher

        public int TeacherId { get; set; }

        public User Teacher { get; set; } = null!;


        // Category

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;


        // Dates

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }


        // Navigation Properties

        public ICollection<Lesson> Lessons { get; set; }= new List<Lesson>();

        public ICollection<Enrollment> Enrollments { get; set; }= new List<Enrollment>();

        public ICollection<Assignment> Assignments { get; set; }= new List<Assignment>();
    }
}
