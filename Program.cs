using TrackingCodeAPI.configs;
using TrackingCodeApi.handlers;
using TrackingCodeApi.models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore; // 👈 necessário para Scalar UI

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// ----------- Serviços -----------
ServicesConfigurator.Configure(builder.Services);
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ----------- Middleware global -----------
// MiddlewareConfigurator.Configure(app);

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;

//     try
//     {
//         var context = services.GetRequiredService<TrackingCodeDb>();

//         if (app.Environment.IsDevelopment())
//         {
//             app.Logger.LogWarning(" Recriando o banco de dados a partir das migrations...");

//             context.Database.EnsureDeleted();  
//             context.Database.Migrate();       

//             app.Logger.LogInformation(" Banco recriado com sucesso a partir das migrations.");
//         }
//         else
//         {
//             if (context.Database.GetPendingMigrations().Any())
//             {
//                 context.Database.Migrate();
//                 app.Logger.LogInformation(" Banco atualizado com migrations pendentes.");
//             }
//             else
//             {
//                 app.Logger.LogInformation(" Nenhuma migration pendente — banco atualizado.");
//             }
//         }
//     }
//     catch (Exception ex)
//     {
//         app.Logger.LogError(ex, " Erro ao aplicar ou recriar o banco de dados.");
//         throw;
//     }
// }


// ----------- Health Check -----------
app.MapGet("/health", () => Results.Ok(new
{
    status = "Healthy",
    timestamp = DateTime.UtcNow
}))
.WithSummary("Health Check")
.WithDescription("Returns 200 OK when the application is running.")
.Produces(StatusCodes.Status200OK)
.ProducesProblem(StatusCodes.Status500InternalServerError);


// ----------- Mapear Handlers -----------
MotoHandler.MapEndpoints(app);
TagHandler.MapEndpoints(app);
SetorHandler.MapEndpoints(app);

// ----------- Swagger / Scalar UI -----------
app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tracking Code API v1"));
}
else
{
    // 👇 Habilita o Scalar UI em produção
    app.MapScalarApiReference(options =>
    {
        options.Title = "Tracking Code API";
        options.Theme = ScalarTheme.Default; // ou .DeepSpace, .Purple, etc.
        options.Servers = new[] { new ScalarServer("https://trackingcodeapi.azurewebsites.net") }; // Ajuste sua URL real
    });
}

// ----------- Outros -----------
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<TrackingCodeApi.Security.ApiKeyMiddleware>();

await app.RunAsync();
