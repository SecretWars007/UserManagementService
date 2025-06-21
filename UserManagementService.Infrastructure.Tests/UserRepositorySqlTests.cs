using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using UserManagementService.Domain.entities;
using UserManagementService.Infrastructure.Persistence.Repositories;

namespace UserManagementService.Infrastructure.Tests
{
    public class UserRepositorySqlTests
    {
        [Fact]
        public async Task Should_Insert_And_Find_User_By_Email_SQLServer()
        {
            // Arrange
            var db = InfrastructureDbContextFactory.CreateSqlServerDb();

            var role = new Role("Tester");
            await new RoleRepository(db).AddAsync(role); // 👈 Este paso es clave

            var email = $"test{Guid.NewGuid()}@correo.com";
            var user = new User(email, "hashed123", role.Id); // usa role.Id válido

            var repo = new UserRepository(db);

            // Act
            await repo.AddAsync(user);
            var result = await repo.GetByEmailAsync(email);

            // Assert
            result.Should().NotBeNull();
            result!.Email.Should().Be(email);
        }
    }
}
