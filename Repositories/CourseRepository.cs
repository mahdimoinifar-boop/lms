using LmsProject.Data;
using LmsProject.Models.Entities;
using LmsProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LmsProject.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(LmsDbContext context)
            : base(context)
        {
        }
        public async Task<IEnumerable<Course>> GetCoursesWithDetailsAsync()
        {
            return await _context.Courses
                .Include(course => course.Teacher)
                .Include(course => course.Category)
                .ToListAsync();
        }

        public async Task<Course?> GetCourseWithDetailsByIdAsync(int id)
        {
            return await _context.Courses
                .Include(course => course.Teacher)
                .Include(course => course.Category)
                .FirstOrDefaultAsync(course => course.CourseId == id);
        }
    }

}