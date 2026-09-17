using LmsProject.DTOs.Lesson;
using LmsProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LmsProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetByCourse(int courseId)
        {
            var result = await _lessonService.GetByCourseIdAsync(courseId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> GetById(int id)
        {
            var result = await _lessonService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("course/{courseId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<LessonDto>> Create(
            int courseId,
            CreateLessonDto dto)
        {
            var teacherId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var result = await _lessonService.CreateAsync(
                courseId,
                teacherId,
                dto
            );

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update(
            int id,
            UpdateLessonDto dto)
        {
            var teacherId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var result = await _lessonService.UpdateAsync(
                id,
                teacherId,
                dto
            );

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            var teacherId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var result = await _lessonService.DeleteAsync(
                id,
                teacherId
            );

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}