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
    public class LoginUserUseCaseTests
    {
        private readonly Mock<IUserRepository> _userRepo = new();
        private readonly Mock<IRoleRepository> _roleRepo = new();
        private readonly Mock<IJwtService> _jwtService = new();

        private readonly LoginUserUseCase _useCase;

        public LoginUserUseCaseTests()
        {
            _useCase = new LoginUserUseCase(_userRepo.Object, _roleRepo.Object, _jwtService.Object);
        }

        [Fact]
        public async Task Should_Login_Successfully()
        {
            // Arrange
            var email = "usuario@example.com";
            var password = "123456";
            var passwordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
            var roleId = Guid.NewGuid();

            var user = new User(email, passwordHash, roleId);
            var role = new Role("Admin");

            _userRepo.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(user);
            _roleRepo.Setup(x => x.GetByIdAsync(roleId)).ReturnsAsync(role);
            _jwtService.Setup(x => x.GenerateToken(user, role.Name)).Returns("fake-jwt-token");

            // Act
            var result = await _useCase.ExecuteAsync(email, password);

            // Assert
            result.Token.Should().Be("fake-jwt-token");
        }

        [Fact]
        public async Task Should_Throw_When_User_Does_Not_Exist()
        {
            // Arrange
            var email = "notfound@example.com";
            var password = "123456";

            _userRepo.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync((User?)null);

            // Act
            var act = async () => await _useCase.ExecuteAsync(email, password);

            // Assert
            await act.Should()
                .ThrowAsync<BusinessException>()
                .WithMessage("Usuario o contraseña incorrectos.");
        }

        [Fact]
        public async Task Should_Throw_When_Password_Is_Incorrect()
        {
            // Arrange
            var email = "usuario@example.com";
            var correctPassword = "123456";
            var wrongPassword = "wrongpass";

            var passwordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(correctPassword));
            var user = new User(email, passwordHash, Guid.NewGuid());

            _userRepo.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(user);

            // Act
            var act = async () => await _useCase.ExecuteAsync(email, wrongPassword);

            // Assert
            await act.Should()
                .ThrowAsync<BusinessException>()
                .WithMessage("Usuario o contraseña incorrectos.");
        }

        [Fact]
        public async Task Should_Throw_When_Role_Not_Found()
        {
            // Arrange
            var email = "usuario@example.com";
            var password = "123456";
            var passwordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
            var roleId = Guid.NewGuid();

            var user = new User(email, passwordHash, roleId);

            _userRepo.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(user);
            _roleRepo.Setup(x => x.GetByIdAsync(roleId)).ReturnsAsync((Role?)null);

            // Act
            var act = async () => await _useCase.ExecuteAsync(email, password);

            // Assert
            await act.Should()
                .ThrowAsync<BusinessException>()
                .WithMessage("Rol no encontrado para el usuario.");
        }
    }
}
