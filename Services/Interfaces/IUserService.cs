using LmsProject.DTOs.User;

namespace LmsProject.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(RegisterDto dto);

        Task<UserDto?> GetByIdAsync(int id);

        Task<UserDto?> GetByUsernameAsync(string username);

        Task<bool> UpdateAsync(int id, UpdateUserDto dto);
    }
}