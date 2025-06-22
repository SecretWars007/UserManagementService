using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using UserManagementService.Application.UsesCases;
using UserManagementService.Domain.entities;
using UserManagementService.Domain.Ports;

namespace UserManagementService.Application.Tests.UsesCases
{
    public class CreateProfileUseCaseTests
    {
        private readonly Mock<IProfileRepository> _profileRepo = new();
        private readonly CreateProfileUseCase _useCase;

        public CreateProfileUseCaseTests()
        {
            _useCase = new CreateProfileUseCase(_profileRepo.Object);
        }

        [Fact]
        public async Task Should_Create_Profile_With_Valid_Data()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var fullName = "Ana Torres";
            var address = "Calle Falsa 123";
            var password = "hashed123";

            _profileRepo.Setup(p => p.AddAsync(It.IsAny<Profile>())).ReturnsAsync((Profile p) => p);

            // Act
            var result = await _useCase.ExecuteAsync(userId, fullName, address, password);

            // Assert
            result.Should().NotBeNull();
            result.UserId.Should().Be(userId);
            result.FullName.Should().Be(fullName);
            result.Address.Should().Be(address);
            _profileRepo.Verify(p => p.AddAsync(It.IsAny<Profile>()), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public async Task Should_Throw_When_FullName_Is_Invalid(string fullName)
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            Func<Task> act = () =>
                _useCase.ExecuteAsync(userId, fullName, "Av. Libre 45", "hashed123");

            // Assert
            await act.Should()
                .ThrowAsync<ArgumentException>()
                .WithMessage("El nombre completo es obligatorio*");
        }

        [Fact]
        public async Task Should_Throw_When_UserId_Is_Empty()
        {
            // Arrange
            var fullName = "Carlos Núñez";

            // Act
            Func<Task> act = () =>
                _useCase.ExecuteAsync(Guid.Empty, fullName, "Calle Central", "hashed123");

            // Assert
            await act.Should()
                .ThrowAsync<ArgumentException>()
                .WithMessage("El ID del usuario es obligatorio*");
        }

        [Fact]
        public async Task Should_Allow_Null_Address()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var fullName = "Lucía Martínez";
            var password = "hashed123";

            _profileRepo.Setup(p => p.AddAsync(It.IsAny<Profile>())).ReturnsAsync((Profile p) => p);

            // Act
            var result = await _useCase.ExecuteAsync(userId, fullName, null, password);

            // Assert
            result.Should().NotBeNull();
            result.Address.Should().BeNull();
        }
    }
}
