using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using UserManagementService.Domain.entities;
using UserManagementService.Infrastructure.Persistence.Repositories;

namespace UserManagementService.Infrastructure.Tests
{
    public class RoleRepositorySqlTests
    {
        [Fact]
        public async Task Should_Insert_And_Get_Role_By_Id()
        {
            // Arrange
            var db = InfrastructureDbContextFactory.CreateSqlServerDb();
            var repo = new RoleRepository(db);

            var role = new Role("Admin");

            // Act
            await repo.AddAsync(role);
            var found = await repo.GetByIdAsync(role.Id);

            // Assert
            found.Should().NotBeNull();
            found!.Name.Should().Be("Admin");
        }

        [Fact]
        public async Task Should_Return_All_Roles()
        {
            var db = InfrastructureDbContextFactory.CreateSqlServerDb();
            var repo = new RoleRepository(db);

            await repo.AddAsync(new Role("Admin"));
            await repo.AddAsync(new Role("User"));

            var roles = await repo.GetAllAsync();

            roles.Count().Should().BeGreaterThan(1);
            roles.Select(r => r.Name).Should().Contain(new[] { "Admin", "User" });
        }
    }
}
