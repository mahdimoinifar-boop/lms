using AutoMapper;
using LmsProject.DTOs.Lesson;
using LmsProject.Models.Entities;
using LmsProject.Repositories.Interfaces;
using LmsProject.Services.Interfaces;

namespace LmsProject.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public LessonService(
            ILessonRepository lessonRepository,
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LessonDto>> GetByCourseIdAsync(int courseId)
        {
            var lessons =await _lessonRepository.GetByCourseIdAsync(courseId);

            return _mapper.Map<IEnumerable<LessonDto>>(lessons);
        }

        public async Task<LessonDto?> GetByIdAsync(int id)
        {
            var lesson =await _lessonRepository.GetByIdAsync(id);

            if (lesson == null)
                return null;

            return _mapper.Map<LessonDto>(lesson);
        }

        public async Task<LessonDto?> CreateAsync(int courseId,int teacherId,CreateLessonDto dto)
        {
            var course =await _courseRepository.GetByIdAsync(courseId);

            if (course == null)
                return null;

            if (course.TeacherId != teacherId)
                return null;

            var lesson =_mapper.Map<Lesson>(dto);

            lesson.CourseId = courseId;

            await _lessonRepository.AddAsync(lesson);

            await _lessonRepository.SaveAsync();

            return _mapper.Map<LessonDto>(lesson);
        }

        public async Task<bool> UpdateAsync(
            int id,
            int teacherId,
            UpdateLessonDto dto)
        {
            var lesson =await _lessonRepository.GetWithCourseAsync(id);

            if (lesson == null)
                return false;

            if (lesson.Course.TeacherId != teacherId)
                return false;

            lesson.Title = dto.Title;
            lesson.Content = dto.Content;
            lesson.VideoUrl = dto.VideoUrl;
            lesson.FileUrl = dto.FileUrl;
            lesson.OnlineClassUrl = dto.OnlineClassUrl;
            lesson.Order = dto.Order;

            _lessonRepository.Update(lesson);

            await _lessonRepository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id,int teacherId)
        {
            var lesson =await _lessonRepository.GetWithCourseAsync(id);

            if (lesson == null)
                return false;

            if (lesson.Course.TeacherId != teacherId)
                return false;

            _lessonRepository.Delete(lesson);

            await _lessonRepository.SaveAsync();

            return true;
        }
    }
}