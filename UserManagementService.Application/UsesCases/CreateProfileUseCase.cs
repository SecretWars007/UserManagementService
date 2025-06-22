using UserManagementService.Domain.entities;
using UserManagementService.Domain.Ports;

namespace UserManagementService.Application.UsesCases
{
    public class CreateProfileUseCase
    {
        private readonly IProfileRepository _profileRepository;

        public CreateProfileUseCase(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<Profile> ExecuteAsync(Guid userId, string fullName, string? address)
        {
            var profile = new Profile(userId, fullName, address);
            return await _profileRepository.AddAsync(profile);
        }
    }
}
