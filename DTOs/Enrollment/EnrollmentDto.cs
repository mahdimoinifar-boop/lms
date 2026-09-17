using LmsProject.Models.Enums;

namespace LmsProject.DTOs.Enrollment
{
    public class EnrollmentDto
    {

        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public int CourseId { get; set; }

        public string CourseTitle { get; set; } = string.Empty;

        public DateTime EnrollmentDate { get; set; }

        public EnrollmentStatus Status { get; set; }

        public double ProgressPercent { get; set; }
    }
}
