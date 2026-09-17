using LmsProject.DTOs.User;
using LmsProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LmsProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginDto dto)
        {
            var result =await _authService.LoginAsync(dto);

            if (result == null)
                return Unauthorized("Invalid username or password.");

            return Ok(result);
        }
    }
}