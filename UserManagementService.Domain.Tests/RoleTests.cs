using System;
using FluentAssertions;
using UserManagementService.Domain.entities;
using Xunit;

namespace UserManagementService.Domain.Tests
{
    public class RoleTests
    {
        [Fact]
        public void CreateRole_WithValidName_ShouldSetName()
        {
            // Arrange
            string name = "Admin";

            // Act
            var role = new Role(name);

            // Assert
            role.Name.Should().Be(name);
            role.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void CreateRole_WithEmptyName_ShouldThrowException()
        {
            // Arrange
            string name = "";

            // Act
            Action act = () => new Role(name);

            // Assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("El nombre del rol no puede estar vacío*");
        }
    }
}
