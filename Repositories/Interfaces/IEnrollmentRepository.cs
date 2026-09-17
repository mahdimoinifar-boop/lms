using LmsProject.Models.Entities;

namespace LmsProject.Repositories.Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        Task<Enrollment?> GetByStudentAndCourseAsync(
            int studentId,
            int courseId);

        Task<int> GetActiveCountByCourseIdAsync(
            int courseId);

        Task<IEnumerable<Enrollment>> GetByStudentIdAsync(
            int studentId);

        Task<IEnumerable<Enrollment>> GetByCourseIdAsync(
            int courseId);
    }
}