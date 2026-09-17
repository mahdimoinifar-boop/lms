using LmsProject.Models.Entities;

namespace LmsProject.Repositories.Interfaces
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Task<IEnumerable<Lesson>> GetByCourseIdAsync(int courseId);

        Task<Lesson?> GetWithCourseAsync(int id);
    }
}