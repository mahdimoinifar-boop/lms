using AutoMapper;
using LmsProject.DTOs.Course;
using LmsProject.Models.Entities;
using LmsProject.Repositories.Interfaces;
using LmsProject.Services.Interfaces;

namespace LmsProject.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var courses =
                await _courseRepository.GetCoursesWithDetailsAsync();

            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto?> GetByIdAsync(int id)
        {
            var course =
                await _courseRepository
                    .GetCourseWithDetailsByIdAsync(id);

            if (course == null)
                return null;

            return _mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> CreateAsync(
            CreateCourseDto dto,
            int teacherId)
        {
            var course = _mapper.Map<Course>(dto);

            course.TeacherId = teacherId;

            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveAsync();

            var createdCourse =
                await _courseRepository
                    .GetCourseWithDetailsByIdAsync(course.CourseId);

            return _mapper.Map<CourseDto>(createdCourse);
        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdateCourseDto dto,
            int teacherId)
        {
            var course =
                await _courseRepository
                    .GetCourseWithDetailsByIdAsync(id);

            if (course == null)
                return false;

            if (course.TeacherId != teacherId)
                return false;

            course.Title = dto.Title;
            course.Description = dto.Description;
            course.CourseCode = dto.CourseCode;
            course.UnitsCount = dto.UnitsCount;
            course.semester = dto.Semester;
            course.Capacity = dto.Capacity;
            course.Price = dto.Price;
            course.ScheduleInfo = dto.ScheduleInfo;
            course.CategoryId = dto.CategoryId;
            course.UpdatedAt = DateTime.UtcNow;

            _courseRepository.Update(course);

            await _courseRepository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(
            int id,
            int teacherId)
        {
            var course =
                await _courseRepository.GetByIdAsync(id);

            if (course == null)
                return false;

            if (course.TeacherId != teacherId)
                return false;

            _courseRepository.Delete(course);

            await _courseRepository.SaveAsync();

            return true;
        }
    }
}