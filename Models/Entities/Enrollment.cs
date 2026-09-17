using LmsProject.Models.Enums;

namespace LmsProject.Models.Entities
{
    public class Enrollment
    {

        public int EnrollmentId { get; set; }


        // Student

        public int StudentId { get; set; }

        public User Student { get; set; } = null!;


        // Course

        public int CourseId { get; set; }

        public Course Course { get; set; } = null!;


        // Enrollment Information

        public DateTime EnrollmentDate { get; set; }= DateTime.UtcNow;

        public EnrollmentStatus Status { get; set; }= EnrollmentStatus.Active;
    }
}
