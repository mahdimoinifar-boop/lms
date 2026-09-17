using LmsProject.DTOs.User;

namespace LmsProject.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginDto dto);
    }
}
