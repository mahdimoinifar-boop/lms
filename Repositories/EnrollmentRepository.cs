using LmsProject.Data;
using LmsProject.Models.Entities;
using LmsProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LmsProject.Repositories
{
    public class EnrollmentRepository
        : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(LmsDbContext context)
            : base(context)
        {
        }

        public async Task<Enrollment?> GetByStudentAndCourseAsync(
            int studentId,
            int courseId)
        {
            return await _context.Enrollments
                .FirstOrDefaultAsync(x =>
                    x.StudentId == studentId &&
                    x.CourseId == courseId);
        }

        public async Task<int> GetActiveCountByCourseIdAsync(
            int courseId)
        {
            return await _context.Enrollments
                .CountAsync(x =>
                    x.CourseId == courseId &&
                    x.Status == Models.Enums.EnrollmentStatus.Active);
        }
        public async Task<IEnumerable<Enrollment>>
            GetByStudentIdAsync(int studentId)
        {
            return await _context.Enrollments
                .Include(x => x.Student)
                .Include(x => x.Course)
                .Where(x => x.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>>
     GetByCourseIdAsync(int courseId)
        {
            return await _context.Enrollments
                .Include(x => x.Student)
                .Include(x => x.Course)
                .Where(x => x.CourseId == courseId)
                .ToListAsync();
        }
    }
}