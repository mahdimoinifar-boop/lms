using AutoMapper;
using LmsProject.DTOs.Assignment;
using LmsProject.Models.Entities;
using LmsProject.Repositories.Interfaces;
using LmsProject.Services.Interfaces;

namespace LmsProject.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public AssignmentService(
            IAssignmentRepository assignmentRepository,
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            _assignmentRepository = assignmentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssignmentDto>> GetByCourseIdAsync(
            int courseId)
        {
            var assignments =
                await _assignmentRepository.GetByCourseIdAsync(courseId);

            return _mapper.Map<IEnumerable<AssignmentDto>>(assignments);
        }

        public async Task<AssignmentDto?> GetByIdAsync(int id)
        {
            var assignment =
                await _assignmentRepository.GetWithCourseAsync(id);

            if (assignment == null)
                return null;

            return _mapper.Map<AssignmentDto>(assignment);
        }

        public async Task<AssignmentDto?> CreateAsync(
            int courseId,
            int teacherId,
            CreateAssignmentDto dto)
        {
            var course =
                await _courseRepository.GetByIdAsync(courseId);

            if (course == null)
                return null;

            if (course.TeacherId != teacherId)
                return null;

            var assignment = _mapper.Map<Assignment>(dto);

            assignment.CourseId = courseId;

            await _assignmentRepository.AddAsync(assignment);
            await _assignmentRepository.SaveAsync();

            var createdAssignment =
                await _assignmentRepository.GetWithCourseAsync(
                    assignment.AssignmentId);

            return _mapper.Map<AssignmentDto>(createdAssignment);
        }

        public async Task<bool> UpdateAsync(
            int id,
            int teacherId,
            UpdateAssignmentDto dto)
        {
            var assignment =
                await _assignmentRepository.GetWithCourseAsync(id);

            if (assignment == null)
                return false;

            if (assignment.Course.TeacherId != teacherId)
                return false;

            assignment.Title = dto.Title;
            assignment.Description = dto.Description;
            assignment.DueDate = dto.DueDate;
            assignment.Priority = dto.Priority;
            assignment.Status = dto.Status;
            assignment.UpdatedAt = DateTime.UtcNow;

            _assignmentRepository.Update(assignment);

            await _assignmentRepository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(
            int id,
            int teacherId)
        {
            var assignment =
                await _assignmentRepository.GetWithCourseAsync(id);

            if (assignment == null)
                return false;

            if (assignment.Course.TeacherId != teacherId)
                return false;

            _assignmentRepository.Delete(assignment);

            await _assignmentRepository.SaveAsync();

            return true;
        }
    }
}