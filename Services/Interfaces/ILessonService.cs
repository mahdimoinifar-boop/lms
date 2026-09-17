using LmsProject.DTOs.Lesson;

namespace LmsProject.Services.Interfaces
{
    public interface ILessonService
    {
        Task<IEnumerable<LessonDto>> GetByCourseIdAsync(int courseId);

        Task<LessonDto?> GetByIdAsync(int id);

        Task<LessonDto?> CreateAsync(
            int courseId,
            int teacherId,
            CreateLessonDto dto);

        Task<bool> UpdateAsync(
            int id,
            int teacherId,
            UpdateLessonDto dto);

        Task<bool> DeleteAsync(
            int id,
            int teacherId);
    }
}