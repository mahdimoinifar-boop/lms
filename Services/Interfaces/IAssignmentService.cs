using LmsProject.DTOs.Assignment;

namespace LmsProject.Services.Interfaces
{
    public interface IAssignmentService
    {
        Task<IEnumerable<AssignmentDto>> GetByCourseIdAsync(int courseId);

        Task<AssignmentDto?> GetByIdAsync(int id);

        Task<AssignmentDto?> CreateAsync(
            int courseId,
            int teacherId,
            CreateAssignmentDto dto);

        Task<bool> UpdateAsync(
            int id,
            int teacherId,
            UpdateAssignmentDto dto);

        Task<bool> DeleteAsync(
            int id,
            int teacherId);
    }
}