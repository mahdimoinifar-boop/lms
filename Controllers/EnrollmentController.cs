using LmsProject.DTOs.Enrollment;
using LmsProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LmsProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(
            IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost("course/{courseId}")]
        [Authorize(Roles = "student")]
        public async Task<ActionResult<EnrollmentDto>> Enroll(
            int courseId)
        {
            var studentId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var result =
                await _enrollmentService.EnrollAsync(
                    studentId,
                    courseId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("my")]
        [Authorize(Roles = "student")]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>>
            GetMyEnrollments()
        {
            var studentId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var result =
                await _enrollmentService
                    .GetMyEnrollmentsAsync(studentId);

            return Ok(result);
        }

        [HttpGet("course/{courseId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>>
            GetCourseEnrollments(int courseId)
        {
            var result =
                await _enrollmentService
                    .GetCourseEnrollmentsAsync(courseId);

            return Ok(result);
        }
    }
}