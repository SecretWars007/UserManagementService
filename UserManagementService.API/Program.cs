using UserManagementService.API.DependencyInjection;
using UserManagementService.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Para OpenAPI
builder.Services.AddSwaggerGen(); // Generador de Swagger

// 🔁 Inyectar dependencias de Application e Infrastructure
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Genera el archivo Swagger
    app.UseSwaggerUI(); // Interfaz Swagger
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
