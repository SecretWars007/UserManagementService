using System;
using FluentAssertions;
using UserManagementService.Domain.entities;
using Xunit;

namespace UserManagementService.Domain.Tests
{
    public class ProfileTests
    {
        [Fact]
        public void CreateProfile_WithValidData_ShouldSetProperties()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var fullName = "Juan Pérez";
            var address = "Av. Siempre Viva 742";

            // Act
            var profile = new Profile(userId, fullName, address);

            // Assert
            profile.UserId.Should().Be(userId);
            profile.FullName.Should().Be(fullName);
            profile.Address.Should().Be(address);
            profile.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void CreateProfile_WithEmptyFullName_ShouldThrow()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var fullName = "";

            // Act
            Action act = () => new Profile(userId, fullName);

            // Assert
            act.Should().Throw<ArgumentException>()
            .WithMessage("El nombre completo es obligatorio*");
        }

        [Fact]
        public void CreateProfile_WithEmptyUserId_ShouldThrow()
        {
            // Arrange
            var userId = Guid.Empty;
            var fullName = "Juan Pérez";

            // Act
            Action act = () => new Profile(userId, fullName);

            // Assert
            act.Should().Throw<ArgumentException>()
            .WithMessage("El ID del usuario es obligatorio*");
        }

        [Fact]
        public void CreateProfile_WithNullAddress_ShouldAllow()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var fullName = "Juan Pérez";

            // Act
            var profile = new Profile(userId, fullName, null);

            // Assert
            profile.Address.Should().BeNull();
        }
    }
}