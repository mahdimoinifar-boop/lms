using LmsProject.Data;
using LmsProject.Models.Entities;
using LmsProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LmsProject.Repositories
{
    public class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(LmsDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Lesson>> GetByCourseIdAsync(
            int courseId)
        {
            return await _context.Lessons
                .Where(x => x.CourseId == courseId)
                .OrderBy(x => x.Order)
                .ToListAsync();
        }

        public async Task<Lesson?> GetWithCourseAsync(int id)
        {
            return await _context.Lessons
                .Include(x => x.Course)
                .FirstOrDefaultAsync(x => x.LessonId == id);
        }
    }
}