using LmsProject.Models.Entities;

namespace LmsProject.Repositories.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetCoursesWithDetailsAsync();
        Task<Course?> GetCourseWithDetailsByIdAsync(int id);
    }
}
