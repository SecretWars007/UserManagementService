using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementService.Domain.entities;
using UserManagementService.Domain.Ports;

namespace UserManagementService.Application.UsesCases
{
    public class GetAllRolesUseCase(IRoleRepository roleRepository)
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<List<Role>> ExecuteAsync()
        {
            return (List<Role>)await _roleRepository.GetAllAsync();
        }
    }
}
