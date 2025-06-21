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
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();

            var connectionString = config.GetConnectionString("TestDb");

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString!)
                .Options;

            var db = new AppDbContext(options);
            db.Database.EnsureCreated(); // para test, mejor que usar migraciones
            return db;
        }
    }
}
