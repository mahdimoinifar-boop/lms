using LmsProject.DTOs.Course;
using LmsProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace LmsProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
        {
            var courses = await _courseService.GetAllAsync();

            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);

            if (course == null)
                return NotFound("Course not found.");

            return Ok(course);
        }
        [HttpPost]
       [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<CourseDto>> Create(CreateCourseDto dto)
        {
            var teacherId =int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var course =await _courseService.CreateAsync(dto,teacherId);

            return CreatedAtAction(nameof(GetById),new { id = course.CourseId },course);
        }
        [HttpPut("{id}")]
       [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update(int id,UpdateCourseDto dto)
        {
            var teacherId =int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result =await _courseService.UpdateAsync(id,dto,teacherId);

            if (!result)
            {
                return NotFound("Course not found or you are not the owner.");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            var teacherId =
                int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _courseService.DeleteAsync(id,teacherId);

            if (!result)
            {
                return NotFound(
                    "Course not found or you are not the owner.");
            }

            return NoContent();
        }
    }
}