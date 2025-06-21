using Microsoft.EntityFrameworkCore;
using UserManagementService.Domain.entities;
using UserManagementService.Domain.Ports;

namespace UserManagementService.Infrastructure.Persistence.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _db;

        public ProfileRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Profile> AddAsync(Profile profile)
        {
            _db.Profiles.Add(profile);
            await _db.SaveChangesAsync();
            return profile;
        }

        public async Task<IEnumerable<Profile>> GetAllAsync()
        {
            return await _db.Profiles.ToListAsync();
        }

        public async Task<Profile?> GetByIdAsync(Guid id)
        {
            return await _db.Profiles.FindAsync(id);
        }

        public Task<Profile?> GetByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
