using LmsProject.Models.Enums;

namespace LmsProject.Models.Entities
{
    public class AssignmentSubmission
    {

        public int SubmissionId { get; set; }


        // Assignment

        public int AssignmentId { get; set; }

        public Assignment Assignment { get; set; } = null!;


        // Student

        public int StudentId { get; set; }

        public User Student { get; set; } = null!;


        // Submission Content

        public string? ResponseContent { get; set; }

        public string? FilePath { get; set; }


        // Submission Information

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public SubmissionStatus Status { get; set; } = SubmissionStatus.Submitted;


        // Grading

        public double? Grade { get; set; }

        public string? Feedback { get; set; }


        // Dates

        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}

