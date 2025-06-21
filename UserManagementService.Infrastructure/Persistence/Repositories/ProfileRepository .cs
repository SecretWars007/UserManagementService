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

        public Task<Profile?> GetByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
