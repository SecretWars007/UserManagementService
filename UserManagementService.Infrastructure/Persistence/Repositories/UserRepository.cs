using Microsoft.EntityFrameworkCore;
using UserManagementService.Domain.entities;
using UserManagementService.Domain.Ports;

namespace UserManagementService.Infrastructure.Persistence.Repositories
{
    public class UserRepository(AppDbContext db) : IUserRepository
    {
        private readonly AppDbContext _db = db;

        public async Task<User?> GetByEmailAsync(string email) =>
            await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User> AddAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }
    }
}
