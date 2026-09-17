using LmsProject.Data;
using LmsProject.Models.Entities;
using LmsProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LmsProject.Repositories
{
    public class AssignmentRepository
        : Repository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(LmsDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Assignment>> GetByCourseIdAsync(
            int courseId)
        {
            return await _context.Assignments
                .Include(assignment => assignment.Course)
                .Where(assignment => assignment.CourseId == courseId)
                .OrderBy(assignment => assignment.DueDate)
                .ToListAsync();
        }

        public async Task<Assignment?> GetWithCourseAsync(int id)
        {
            return await _context.Assignments
                .Include(assignment => assignment.Course)
                .FirstOrDefaultAsync(
                    assignment => assignment.AssignmentId == id);
        }
    }
}