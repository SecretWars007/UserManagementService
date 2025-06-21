using Microsoft.EntityFrameworkCore;
using UserManagementService.Domain.entities;
using UserManagementService.Domain.Ports;

namespace UserManagementService.Infrastructure.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _db;

        public RoleRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Role> AddAsync(Role role)
        {
            _db.Roles.Add(role);
            await _db.SaveChangesAsync();
            return role;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _db.Roles.ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(Guid id) => await _db.Roles.FindAsync(id);
    }
}
