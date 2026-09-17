using LmsProject.Models.Enums;

namespace LmsProject.DTOs.Assignment
{
    public class AssignmentDto
    {

        public int AssignmentId { get; set; }

        public int CourseId { get; set; }

        public string CourseTitle { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public AssignmentPriority Priority { get; set; }

        public AssignmentStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
