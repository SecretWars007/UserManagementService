using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserManagementService.Infrastructure.Persistence;

namespace UserManagementService.Infrastructure.Tests
{
    public static class InfrastructureDbContextFactory
    {
        public static AppDbContext CreateSqlServerDb()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Test.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("TestDb");

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var context = new AppDbContext(options);

            // 💥 Forzamos eliminar la base antes de crearla
            //context.Database.EnsureDeleted(); // borra si existe
            //context.Database.EnsureCreated(); // vuelve a crear

            return context;
        }
    }
}
