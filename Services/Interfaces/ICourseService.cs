using LmsProject.DTOs.Course;

namespace LmsProject.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllAsync();

        Task<CourseDto?> GetByIdAsync(int id);

        Task<CourseDto> CreateAsync( CreateCourseDto dto,int teacherId);

        Task<bool> UpdateAsync(int id,UpdateCourseDto dto,int teacherId);

        Task<bool> DeleteAsync(int id,int teacherId);
    }
}