using UserManagementService.API.DependencyInjection;
using UserManagementService.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Para OpenAPI
builder.Services.AddSwaggerGen(); // Generador de Swagger

// ✅ Agrega política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 🔁 Inyectar dependencias de Application e Infrastructure
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// ✅ Usa CORS
app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

// ⚠️ Usa HTTP si estás en desarrollo (quítalo si quieres forzar HTTPS solo en producción)
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
