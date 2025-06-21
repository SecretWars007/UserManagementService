using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using UserManagementService.Application.UsesCases;
using UserManagementService.Domain.entities;
using UserManagementService.Domain.Exceptions;
using UserManagementService.Domain.Ports;

namespace UserManagementService.Application.Tests
{
    public class RegisterUserUseCaseTests
    {
        private readonly Mock<IUserRepository> _userRepo = new();
        private readonly Mock<IProfileRepository> _profileRepo = new();
        private readonly RegisterUserUseCase _useCase;

        public RegisterUserUseCaseTests()
        {
            _useCase = new RegisterUserUseCase(_userRepo.Object, _profileRepo.Object);
        }

        [Fact]
        public async Task Should_Register_User_Successfully()
        {
            // Arrange
            var email = "nuevo@correo.com";
            var passwordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes("123456"));
            var roleId = Guid.NewGuid();
            var fullName = "Juan Pérez";
            var address = "Calle 123";

            _userRepo.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync((User?)null);
            _userRepo.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync((User u) => u);
            _profileRepo.Setup(p => p.AddAsync(It.IsAny<Profile>())).ReturnsAsync((Profile p) => p);

            // Act
            var result = await _useCase.ExecuteAsync(
                email,
                passwordHash,
                roleId,
                fullName,
                address
            );

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(email);
            result.RoleId.Should().Be(roleId);

            _userRepo.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
            _profileRepo.Verify(p => p.AddAsync(It.IsAny<Profile>()), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_When_Email_Is_Invalid()
        {
            // Arrange
            var email = "correo-malo";
            var passwordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes("123456"));
            var roleId = Guid.NewGuid();
            var fullName = "Juan Pérez";

            // Act
            Func<Task> act = () =>
                _useCase.ExecuteAsync(email, passwordHash, roleId, fullName, null);

            // Assert
            await act.Should()
                .ThrowAsync<BusinessException>()
                .WithMessage("El email no tiene un formato válido.");
        }

        [Fact]
        public async Task Should_Throw_When_Email_Already_Exists()
        {
            // Arrange
            var email = "existe@correo.com";
            var passwordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes("123456"));
            var roleId = Guid.NewGuid();
            var fullName = "Usuario existente";

            var existingUser = new User(email, passwordHash, roleId);
            _userRepo.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync(existingUser);

            // Act
            Func<Task> act = () =>
                _useCase.ExecuteAsync(email, passwordHash, roleId, fullName, null);

            // Assert
            await act.Should()
                .ThrowAsync<BusinessException>()
                .WithMessage("Ya existe un usuario con ese email.");
        }
    }
}
