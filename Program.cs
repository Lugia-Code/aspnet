using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TrackingCodeAPI.configs;
using TrackingCodeApi.handlers;
using TrackingCodeApi.models;
using TrackingCodeApi.Security;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// ----------- Serviços -----------
ServicesConfigurator.Configure(builder.Services, builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----------- Configuração do banco -----------
var connectionString = "Server=serverdb2;Database=TrackingCodeDB;User Id=adminsql;Password=Senha123!;TrustServerCertificate=True;";
builder.Services.AddDbContext<TrackingCodeDb>(options =>
    options.UseSqlServer(connectionString)
);

// ----------- App -----------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapScalarApiReference();
}

app.UseMiddleware<ApiKeyMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
