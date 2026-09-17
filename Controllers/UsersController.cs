using LmsProject.DTOs.User;
using LmsProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LmsProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            try
            {
                var user = await _userService.RegisterAsync(dto);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,UpdateUserDto dto)
        {
            var result =
                await _userService.UpdateAsync(id, dto);

            if (!result)
                return NotFound("User not found.");

            return NoContent();
        }
    }
}