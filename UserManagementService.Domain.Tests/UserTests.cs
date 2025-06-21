using System;
using FluentAssertions;
using UserManagementService.Domain.entities;
using Xunit;

namespace UserManagementService.Domain.Tests
{
    public class UserTests
    {
        [Fact]
        public void CreateUser_WithValidData_ShouldSetProperties()
        {
            // Arrange
            string email = "usuario@example.com";
            string passwordHash = "hash123";
            Guid roleId = Guid.NewGuid();

            // Act
            var user = new User(email, passwordHash, roleId);

            // Assert
            user.Email.Should().Be(email);
            user.PasswordHash.Should().Be(passwordHash);
            user.RoleId.Should().Be(roleId);
            user.Id.Should().NotBeEmpty();
        }
    }
}
