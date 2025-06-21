using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementService.Domain.entities;
using UserManagementService.Domain.Exceptions;
using UserManagementService.Domain.Ports;

namespace UserManagementService.Application.UsesCases
{
    public class CreateRoleUseCase
    {
        private readonly IRoleRepository _roleRepository;

        public CreateRoleUseCase(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> ExecuteAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BusinessException("El nombre del rol es obligatorio.");

            var role = new Role(name);
            return await _roleRepository.AddAsync(role);
        }
    }
}
