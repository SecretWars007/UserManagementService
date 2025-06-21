using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserManagementService.Domain.entities;
using UserManagementService.Domain.Exceptions;
using UserManagementService.Domain.Ports;

namespace UserManagementService.Application.UsesCases
{
    public class RegisterUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;

        public RegisterUserUseCase(
            IUserRepository userRepository,
            IProfileRepository profileRepository
        )
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
        }

        public async Task<User> ExecuteAsync(
            string email,
            string passwordHash,
            Guid roleId,
            string fullName,
            string? address
        )
        {
            // 1. Validar email
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new BusinessException("El email no tiene un formato válido.");

            // 2. Validar que no exista usuario
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
                throw new BusinessException("Ya existe un usuario con ese email.");

            // 3. Crear usuario
            var user = new User(email, passwordHash, roleId);
            var createdUser = await _userRepository.AddAsync(user);

            // 4. Crear perfil
            var profile = new Profile(createdUser.Id, fullName, address);
            await _profileRepository.AddAsync(profile);

            return createdUser;
        }
    }
}
