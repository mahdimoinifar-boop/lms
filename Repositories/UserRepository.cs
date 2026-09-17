using LmsProject.Data;
using LmsProject.Models.Entities;
using LmsProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LmsProject.Repositories
{

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(LmsDbContext context)
            : base(context)
        {
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(User => User.Username == username);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(User => User.Email == email);
        }
    }
}
