using AutoMapper;
using LmsProject.DTOs.User;
using LmsProject.Models.Entities;
using LmsProject.Models.Enums;
using LmsProject.Repositories.Interfaces;
using LmsProject.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LmsProject.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository,IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            var existingUsername =await _userRepository.GetByUsernameAsync(dto.Username);

            if (existingUsername != null)
                throw new Exception("Username already exists.");

            var existingEmail =await _userRepository.GetByEmailAsync(dto.Email);

            if (existingEmail != null)
                throw new Exception("Email already exists.");

            var user = _mapper.Map<User>(dto);

          
            user.IsActive = true;

            user.PasswordHash =_passwordHasher.HashPassword(user,dto.Password);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> GetByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
                return null;

            return _mapper.Map<UserDto>(user);
        }
        public async Task<bool> UpdateAsync(
           int id,
           UpdateUserDto dto)
        {
            var user =await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            user.Fullname = dto.Fullname;
            user.Email = dto.Email;

            _userRepository.Update(user);

            await _userRepository.SaveAsync();

            return true;
        }
    }
}
