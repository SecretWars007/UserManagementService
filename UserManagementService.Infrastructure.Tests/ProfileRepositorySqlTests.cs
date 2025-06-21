using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using UserManagementService.Domain.entities;
using UserManagementService.Infrastructure.Persistence.Repositories;

namespace UserManagementService.Infrastructure.Tests
{
    public class ProfileRepositorySqlTests
    {
        [Fact]
        public async Task Should_Insert_And_Get_Profile_By_Id()
        {
            var db = InfrastructureDbContextFactory.CreateSqlServerDb();

            // 🔑 Primero insertamos un rol válido
            var role = new Role("Tester");
            var roleRepo = new RoleRepository(db);
            await roleRepo.AddAsync(role);

            // 👤 Luego creamos un usuario que lo use
            var email = $"test_{Guid.NewGuid()}@correo.com";
            var user = new User(email, "hash123", role.Id);
            var userRepo = new UserRepository(db);
            await userRepo.AddAsync(user);

            // 🧑‍💼 Finalmente insertamos un profile vinculado al usuario
            var profile = new Profile(user.Id, "Juan Pérez", "Av. Central");
            var profileRepo = new ProfileRepository(db);
            await profileRepo.AddAsync(profile);

            var result = await profileRepo.GetByIdAsync(profile.Id);

            result.Should().NotBeNull();
            result!.FullName.Should().Be("Juan Pérez");
        }

        [Fact]
        public async Task Should_Return_All_Profiles()
        {
            var db = InfrastructureDbContextFactory.CreateSqlServerDb();

            // 1. Crear un rol válido
            var role = new Role("Tester");
            await db.Roles.AddAsync(role);
            await db.SaveChangesAsync();

            // 2. Crear un usuario con ese rol
            var email = $"test_{Guid.NewGuid()}@correo.com";
            var user = new User(email, "hashed123", role.Id);
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            // 3. Insertar perfiles para ese usuario
            var profile1 = new Profile(user.Id, "Juan Pérez", "Zona Norte");
            var profile2 = new Profile(user.Id, "Ana Gómez", "Zona Sur");

            var repo = new ProfileRepository(db);
            await repo.AddAsync(profile1);
            await repo.AddAsync(profile2);

            // 4. Act
            var profiles = await repo.GetAllAsync();

            // 5. Assert
            profiles.Should().HaveCountGreaterThan(1);
            profiles.Select(p => p.FullName).Should().Contain(new[] { "Juan Pérez", "Ana Gómez" });
        }
    }
}
