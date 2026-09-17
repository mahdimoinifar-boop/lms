using LmsProject.Models.Entities;

namespace LmsProject.Repositories.Interfaces
{

    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
    }
}
