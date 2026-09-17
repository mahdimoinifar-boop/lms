using LmsProject.Models.Entities;

namespace LmsProject.Repositories.Interfaces
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        Task<IEnumerable<Assignment>> GetByCourseIdAsync(int courseId);

        Task<Assignment?> GetWithCourseAsync(int id);
    }
}