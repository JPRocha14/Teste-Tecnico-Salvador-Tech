using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VagasAPI.Models; // Alterado para refletir a namespace correta do seu contexto

var builder = WebApplication.CreateBuilder(args);

// Configurar conexão com SQL Server
builder.Services.AddDbContext<VagasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar controllers
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "VagasAPI",
        Version = "v1",
        Description = "API para gerenciamento de vagas"
    });
});

// Configurar o middleware
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Ativar Swagger no ambiente de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "VagasAPI v1");
    });
}

// Middleware padrão
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
