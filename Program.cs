using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TrackingCodeAPI.configs;
using TrackingCodeApi.handlers;
using TrackingCodeApi.models;
using TrackingCodeApi.Security;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------------
// 🔧 CONFIGURAÇÃO PRINCIPAL
// --------------------------------------------------------
var configuration = builder.Configuration;

// Adiciona o DbContext com a Connection String do appsettings.json
builder.Services.AddDbContext<TrackingCodeDb>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
);

// Configura os serviços da aplicação
ServicesConfigurator.Configure(builder.Services, configuration);

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --------------------------------------------------------
// 🧱 APLICAÇÃO DE MIGRATIONS AUTOMÁTICA
// --------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<TrackingCodeDb>();

        if (app.Environment.IsDevelopment())
        {
            app.Logger.LogWarning("⚙️ Recriando o banco de dados a partir das migrations...");
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            app.Logger.LogInformation("✅ Banco recriado com sucesso.");
        }
        else
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                app.Logger.LogInformation("📦 Aplicando migrations pendentes...");
                context.Database.Migrate();
                app.Logger.LogInformation("✅ Banco atualizado.");
            }
            else
            {
                app.Logger.LogInformation("✅ Nenhuma migration pendente — banco já atualizado.");
            }
        }
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "❌ Erro a
