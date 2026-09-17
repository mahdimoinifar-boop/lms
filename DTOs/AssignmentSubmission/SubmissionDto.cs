using LmsProject.Models.Enums;

namespace LmsProject.DTOs.AssignmentSubmission
{
    public class SubmissionDto
    {
        public int SubmissionId { get; set; }

        public int AssignmentId { get; set; }

        public string AssignmentTitle { get; set; } = string.Empty;

        public int StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public string? ResponseContent { get; set; }

        public string? FilePath { get; set; }

        public DateTime SubmittedAt { get; set; }

        public SubmissionStatus Status { get; set; }

        public double? Grade { get; set; }

        public string? Feedback { get; set; }
    }
}
