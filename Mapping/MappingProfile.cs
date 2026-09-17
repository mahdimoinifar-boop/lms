using AutoMapper;
using LmsProject.DTOs.Assignment;
using LmsProject.DTOs.AssignmentSubmission;
using LmsProject.DTOs.Category;
using LmsProject.DTOs.Course;
using LmsProject.DTOs.Enrollment;
using LmsProject.DTOs.Lesson;
using LmsProject.DTOs.User;
using LmsProject.Models.Entities;

namespace LmsProject.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>();

            // Category
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();

            // Course
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.TeacherName,opt => opt.MapFrom(src => src.Teacher.Fullname))
                .ForMember(dest => dest.CategoryName,opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();

            // Lesson
            CreateMap<Lesson, LessonDto>();
            CreateMap<CreateLessonDto, Lesson>();
            CreateMap<UpdateLessonDto, Lesson>();



            //52


            // Enrollment
            CreateMap<Enrollment, EnrollmentDto>()
                .ForMember(dest => dest.StudentName,opt => opt.MapFrom(src => src.Student.Fullname))
                .ForMember(dest => dest.CourseTitle,opt => opt.MapFrom(src => src.Course.Title));

            // Assignment
            CreateMap<Assignment, AssignmentDto>()
                .ForMember(dest => dest.CourseTitle,opt => opt.MapFrom(src => src.Course.Title));

            CreateMap<CreateAssignmentDto, Assignment>();
            CreateMap<UpdateAssignmentDto, Assignment>();

            // Assignment Submission
            CreateMap<AssignmentSubmission, SubmissionDto>()
                .ForMember( dest => dest.AssignmentTitle,opt => opt.MapFrom(src => src.Assignment.Title))
                .ForMember(dest => dest.StudentName,opt => opt.MapFrom(src => src.Student.Fullname));

            CreateMap<CreateSubmissionDto, AssignmentSubmission>();
        }
    }
}