using DbModel.sistemaVentas;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Business.Producto;
using SistemaVentas.Repository.ProductoRepo.Contratos;
using SistemaVentas.Repository.ProductoRepo.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Configurar la conexión a la base de datos
builder.Services.AddDbContext<_sistemaVentasContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("sistemaVentasDb");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Inyección de dependencias - Repositories
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

// Inyección de dependencias - Business Logic
builder.Services.AddScoped<IProductoBusiness, ProductoBusiness>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();