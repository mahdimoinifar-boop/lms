using LmsProject.DTOs.Assignment;
using LmsProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LmsProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentsController(
            IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<AssignmentDto>>>
            GetByCourse(int courseId)
        {
            var result =
                await _assignmentService.GetByCourseIdAsync(courseId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentDto>>
            GetById(int id)
        {
            var result =
                await _assignmentService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("course/{courseId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<AssignmentDto>>
            Create(
                int courseId,
                CreateAssignmentDto dto)
        {
            var teacherId =
                int.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier)!);

            var result =
                await _assignmentService.CreateAsync(
                    courseId,
                    teacherId,
                    dto);

            if (result == null)
                return BadRequest(
                    "Course not found or you are not the teacher of this course.");

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update(
            int id,
            UpdateAssignmentDto dto)
        {
            var teacherId =
                int.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier)!);

            var result =
                await _assignmentService.UpdateAsync(
                    id,
                    teacherId,
                    dto);

            if (!result)
                return BadRequest(
                    "Assignment not found or you are not the teacher of this course.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            var teacherId =
                int.Parse(
                    User.FindFirstValue(
                        ClaimTypes.NameIdentifier)!);

            var result =
                await _assignmentService.DeleteAsync(
                    id,
                    teacherId);

            if (!result)
                return BadRequest(
                    "Assignment not found or you are not the teacher of this course.");

            return NoContent();
        }
    }
}