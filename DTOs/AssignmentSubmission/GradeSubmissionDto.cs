using System.ComponentModel.DataAnnotations;

namespace LmsProject.DTOs.AssignmentSubmission
{
    public class GradeSubmissionDto
    {

        [Range(0, 20)]
        public double Grade { get; set; }

        public string? Feedback { get; set; }
    }
}
