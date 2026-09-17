using LmsProject.DTOs.Enrollment;

namespace LmsProject.Services.Interfaces
{
    public interface IEnrollmentService
    {
        Task<EnrollmentDto?> EnrollAsync(
            int studentId,
            int courseId);

        Task<IEnumerable<EnrollmentDto>> GetMyEnrollmentsAsync(
            int studentId);

        Task<IEnumerable<EnrollmentDto>> GetCourseEnrollmentsAsync(
            int courseId);
    }
}