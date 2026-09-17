using AutoMapper;
using LmsProject.DTOs.Enrollment;
using LmsProject.Models.Entities;
using LmsProject.Repositories.Interfaces;
using LmsProject.Services.Interfaces;
using LmsProject.Models.Enums;
namespace LmsProject.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public EnrollmentService(
            IEnrollmentRepository enrollmentRepository,
            ICourseRepository courseRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<EnrollmentDto?> EnrollAsync(
            int studentId,
            int courseId)
        {
            var student =
                await _userRepository.GetByIdAsync(studentId);

            if (student == null)
                return null;

            if (student.Role != UserRole.student)
                return null;


            var course =
                await _courseRepository.GetByIdAsync(courseId);

            if (course == null)
                return null;

            var existingEnrollment =
                await _enrollmentRepository
                    .GetByStudentAndCourseAsync(
                        studentId,
                        courseId);

            if (existingEnrollment != null)
                throw new Exception(
                    "Student is already enrolled in this course.");

            if (course.Capacity.HasValue)
            {
                var activeCount =
                    await _enrollmentRepository
                        .GetActiveCountByCourseIdAsync(courseId);

                if (activeCount >= course.Capacity.Value)
                    throw new Exception(
                        "Course capacity is full.");
            }

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId
            };

            await _enrollmentRepository.AddAsync(enrollment);

            await _enrollmentRepository.SaveAsync();

            return _mapper.Map<EnrollmentDto>(enrollment);
        }

        public async Task<IEnumerable<EnrollmentDto>>
            GetMyEnrollmentsAsync(int studentId)
        {
            var enrollments =
                await _enrollmentRepository
                    .GetByStudentIdAsync(studentId);

            return _mapper.Map<IEnumerable<EnrollmentDto>>(
                enrollments);
        }

        public async Task<IEnumerable<EnrollmentDto>>
            GetCourseEnrollmentsAsync(int courseId)
        {
            var enrollments =
                await _enrollmentRepository
                    .GetByCourseIdAsync(courseId);

            return _mapper.Map<IEnumerable<EnrollmentDto>>(
                enrollments);
        }
    }
}