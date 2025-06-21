# DESARROLLO DE SCAFFOLD DEL MICROSERVICIO DE GESTION DE USUARIOS CON C# ARQUITECTURA HEXAGONAL
1 - CREACION DE DIRECTORIO RAIZ
    mkdir UserManagementService
    cd UserManagementService
2 - CREACION DE SOLUCION RAIZ
    dotnet new sln -n UserManagementService
3 - CREACION DE PROYECTOS
    # Capa Dominio (Entidades, interfaces)
    dotnet new classlib -n UserManagement.Domain

    # Capa Aplicación (casos de uso)
    dotnet new classlib -n UserManagement.Application

    # Capa Infraestructura (repositorios, base de datos)
    dotnet new classlib -n UserManagement.Infrastructure

    # Capa API (adaptador HTTP, Web API)
    dotnet new webapi -n UserManagement.API
4 - CREACION DE REFERENCIAS ENTRE PROYECTOS
    # Domain lo usan Application e Infrastructure
    dotnet add UserManagement.Application/UserManagement.Application.csproj reference UserManagement.Domain/UserManagement.Domain.csproj
    dotnet add UserManagement.Infrastructure/UserManagement.Infrastructure.csproj reference UserManagement.Domain/UserManagement.Domain.csproj

    # API depende de Application e Infrastructure
    dotnet add UserManagement.API/UserManagement.API.csproj reference UserManagement.Application/UserManagement.Application.csproj
    dotnet add UserManagement.API/UserManagement.API.csproj reference UserManagement.Infrastructure/UserManagement.Infrastructure.csproj
5 - CREACION DE REFERENCIAS DE TODOS LOS PROYECTOS AL PROYECTO RAIZ
    dotnet sln add UserManagement.Domain/UserManagement.Domain.csproj
    dotnet sln add UserManagement.Application/UserManagement.Application.csproj
    dotnet sln add UserManagement.Infrastructure/UserManagement.Infrastructure.csproj
    dotnet sln add UserManagement.API/UserManagement.API.csproj
