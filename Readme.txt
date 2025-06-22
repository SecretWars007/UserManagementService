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
6 - INSTALAR DEPENDENCIAS NUGET
    dotnet add UserManagement.API package Swashbuckle.AspNetCore
    dotnet add UserManagementService.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add UserManagementService.Infrastructure package Microsoft.EntityFrameworkCore
    dotnet add UserManagementService.Infrastructure package Microsoft.EntityFrameworkCore.Tools
    dotnet add UserManagementService.Infrastructure.Tests package Microsoft.Extensions.Configuration
    dotnet add UserManagementService.Infrastructure.Tests package Microsoft.Extensions.Configuration.Json

7 - REALIZAR EL BUILD
    dotnet build
8 - EJECUTAR EL PROYECTO
    dotnet run --project UserManagement.API
9 - CREACION DE PROYECTOS TEST CON TDD
    dotnet new xunit -n UserManagement.Domain.Tests
    dotnet new xunit -n UserManagement.Application.Tests
    dotnet new xunit -n UserManagement.Infrastructure.Tests
    dotnet new xunit -n UserManagement.API.Tests
10 - CREACION DE REFERENCIAS PROYECTOS TEST TDD
    dotnet add UserManagement.Domain.Tests/UserManagement.Domain.Tests.csproj reference UserManagement.Domain/UserManagement.Domain.csproj
    dotnet add UserManagement.Application.Tests/UserManagement.Application.Tests.csproj reference UserManagement.Application/UserManagement.Application.csproj
    dotnet add UserManagement.Application.Tests/UserManagement.Application.Tests.csproj reference UserManagement.Domain/UserManagement.Domain.csproj
    dotnet add UserManagement.Infrastructure.Tests/UserManagement.Infrastructure.Tests.csproj reference UserManagement.Infrastructure/UserManagement.Infrastructure.csproj
    dotnet add UserManagement.Infrastructure.Tests/UserManagement.Infrastructure.Tests.csproj reference UserManagement.Domain/UserManagement.Domain.csproj
    dotnet add UserManagement.API.Tests/UserManagement.API.Tests.csproj reference UserManagement.API/UserManagement.API.csproj
    dotnet add UserManagement.API.Tests/UserManagement.API.Tests.csproj reference UserManagement.Application/UserManagement.Application.csproj
    dotnet add UserManagement.API.Tests/UserManagement.API.Tests.csproj reference UserManagement.Domain/UserManagement.Domain.csproj
11 - AGREGAR PAQUETES NUGET
    dotnet add UserManagement.Domain.Tests package xunit
    dotnet add UserManagement.Application.Tests package xunit
    dotnet add UserManagement.Application.Tests package Moq
    dotnet add UserManagement.Infrastructure.Tests package xunit
    dotnet add UserManagement.API.Tests package xunit
    dotnet add UserManagement.Domain.Tests package FluentAssertions
    dotnet add UserManagementService.Application.Tests package FluentAssertions
    dotnet add UserManagementService.API package Microsoft.EntityFrameworkCore.Design
12 - AGREGAR REFERENCIAS DEL PROYECTO TEST TDD AL PROYECTO RAIZ
    dotnet sln add UserManagement.Domain.Tests/UserManagement.Domain.Tests.csproj
    dotnet sln add UserManagement.Application.Tests/UserManagement.Application.Tests.csproj
    dotnet sln add UserManagement.Infrastructure.Tests/UserManagement.Infrastructure.Tests.csproj
    dotnet sln add UserManagement.API.Tests/UserManagement.API.Tests.csproj
13 - REALIZAR EL BUILD
    dotnet build
14 - REALIZAR MIGRACION A MOTOR SQL SQLSERVER
    dotnet ef migrations add InitialCreate --project UserManagementService.Infrastructure --startup-project UserManagementService.API
    dotnet ef database update --project UserManagementService.Infrastructure --startup-project UserManagementService.API

