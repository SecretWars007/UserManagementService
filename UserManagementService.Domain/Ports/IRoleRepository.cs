using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementService.Domain.entities;

namespace UserManagementService.Domain.Ports
{
    public interface IRoleRepository
    {
        Task<Role> AddAsync(Role role);
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role?> GetByIdAsync(Guid id);
    }
}
