using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementService.Application.UsesCases;

namespace UserManagementService.API.DependencyInjection
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registrar casos de uso (use cases)
            services.AddScoped<RegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<CreateRoleUseCase, CreateRoleUseCase>();
            services.AddScoped<CreateProfileUseCase, CreateProfileUseCase>();
            services.AddScoped<GetAllRolesUseCase, GetAllRolesUseCase>();
            return services;
        }
    }
}
