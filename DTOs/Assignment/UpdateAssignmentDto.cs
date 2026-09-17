using LmsProject.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace LmsProject.DTOs.Assignment
{
    public class UpdateAssignmentDto
    {
       [ Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public AssignmentPriority Priority { get; set; }

        public AssignmentStatus Status { get; set; }
    }
}
