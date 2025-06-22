using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagementService.Domain.Ports;
using UserManagementService.Infrastructure.Persistence;
using UserManagementService.Infrastructure.Persistence.Repositories;

namespace UserManagementService.API.Extensions
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            // Configura DbContext con cadena de conexión
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DBConfiguration"))
            );

            // Registra repositorios
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();

            return services;
        }
    }
}
