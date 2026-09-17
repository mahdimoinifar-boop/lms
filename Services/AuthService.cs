using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LmsProject.DTOs.User;
using LmsProject.Models.Entities;
using LmsProject.Repositories.Interfaces;
using LmsProject.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace LmsProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
        {
            var user =await _userRepository.GetByUsernameAsync(dto.Username);

            if (user == null)
                return null;

            if (!user.IsActive)
                return null;

            var passwordResult = _passwordHasher.VerifyHashedPassword(user,user.PasswordHash,dto.Password);

            if (passwordResult == PasswordVerificationResult.Failed)
                return null;

            var token = GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                UserId = user.UserId,
                Username = user.Username,
                Role = user.Role.ToString()
            };
        }

        private string GenerateToken(User user)
        {
            var jwtKey =_configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(jwtKey))
                throw new Exception("JWT key is not configured.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),

                new Claim(ClaimTypes.Name,user.Username),

                new Claim(ClaimTypes.Role,user.Role.ToString())
            };

            var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}