using LmsProject.Models.Enums;

namespace LmsProject.Models.Entities
{
    public class Assignment
    {

        public int AssignmentId { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; } = null!;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public AssignmentPriority Priority { get; set; } = AssignmentPriority.Medium;

        public AssignmentStatus Status { get; set; }= AssignmentStatus.Active;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }


        // Navigation Property

        public ICollection<AssignmentSubmission> Submissions { get; set; } = new List<AssignmentSubmission>();
    }
}
