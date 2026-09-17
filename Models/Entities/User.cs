using LmsProject.Models.Enums;
namespace LmsProject.Models.Entities
{
    public class User
    {
        public int UserId { get; set; } 
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { set; get; } = string.Empty;

        public string Fullname { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.student;



        public bool IsActive { get; set; }= true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Course> CoursesTaught { get; set; } = new List<Course>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<AssignmentSubmission> Submissions { get; set; } = new List<AssignmentSubmission>();














    }

}
