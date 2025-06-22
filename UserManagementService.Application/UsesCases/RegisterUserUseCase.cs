using System.Text.RegularExpressions;
using UserManagementService.Domain.entities;
using UserManagementService.Domain.Exceptions;
using UserManagementService.Domain.Ports;

namespace UserManagementService.Application.UsesCases
{
    public partial class RegisterUserUseCase(
        IUserRepository userRepository,
        IProfileRepository profileRepository
    )
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IProfileRepository _profileRepository = profileRepository;

        public async Task<User> ExecuteAsync(
            string email,
            string passwordHash,
            Guid roleId,
            string fullName,
            string? address
        )
        {
            // 1. Validar email
            if (!MyRegex().IsMatch(email))
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

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial Regex MyRegex();
    }
}
