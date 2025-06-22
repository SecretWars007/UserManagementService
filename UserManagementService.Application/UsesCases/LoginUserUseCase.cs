using System.Text;
using UserManagementService.Application.DTOs;
using UserManagementService.Domain.Exceptions;
using UserManagementService.Domain.Ports;

namespace UserManagementService.Application.UsesCases
{
    public class LoginUserUseCase(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IJwtService jwtService
    )
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IJwtService _jwtService = jwtService;

        public async Task<AuthToken> ExecuteAsync(string email, string password)
        {
            var user =
                await _userRepository.GetByEmailAsync(email)
                ?? throw new BusinessException("Usuario o contraseña incorrectos.");

            var passwordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

            if (user.PasswordHash != passwordHash)
                throw new BusinessException("Usuario o contraseña incorrectos.");

            var role =
                await _roleRepository.GetByIdAsync(user.RoleId)
                ?? throw new BusinessException("Rol no encontrado para el usuario.");

            var token = _jwtService.GenerateToken(user, role.Name);
            return new AuthToken(token);
        }
    }
}
