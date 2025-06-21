using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementService.Domain.entities;

namespace UserManagementService.Domain.Ports
{
    public interface IProfileRepository
    {
        Task<Profile> AddAsync(Profile profile);
        Task<Profile?> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Profile>> GetAllAsync();
    }
}
