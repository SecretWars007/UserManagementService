using FluentAssertions;
using Moq;
using UserManagementService.Application.UsesCases;
using UserManagementService.Domain.entities;
using UserManagementService.Domain.Exceptions;
using UserManagementService.Domain.Ports;

namespace UserManagementService.Application.Tests.UsesCases
{
    public class CreateRoleUseCaseTests
    {
        private readonly Mock<IRoleRepository> _roleRepository = new();
        private readonly CreateRoleUseCase _useCase;

        public CreateRoleUseCaseTests()
        {
            _useCase = new CreateRoleUseCase(_roleRepository.Object);
        }

        [Fact]
        public async Task Should_Create_Role_With_Valid_Name()
        {
            // Arrange
            string roleName = "Administrador";
            var expectedRole = new Role(roleName);

            _roleRepository.Setup(r => r.AddAsync(It.IsAny<Role>())).ReturnsAsync((Role r) => r);

            // Act
            var result = await _useCase.ExecuteAsync(roleName);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(roleName);
            result.Id.Should().NotBeEmpty();

            _roleRepository.Verify(r => r.AddAsync(It.IsAny<Role>()), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async Task Should_Throw_When_RoleName_Is_Invalid(string invalidName)
        {
            // Act
            Func<Task> act = () => _useCase.ExecuteAsync(invalidName);

            // Assert
            await act.Should()
                .ThrowAsync<BusinessException>()
                .WithMessage("El nombre del rol es obligatorio.");
        }
    }
}
