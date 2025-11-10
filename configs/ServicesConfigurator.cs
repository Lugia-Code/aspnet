using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FluentValidation;
using System.Collections.Generic;

using TrackingCodeApi.repos.auditoria;
using TrackingCodeApi.repos.localizacao;
using TrackingCodeApi.repos.moto;
using TrackingCodeApi.repos.setor;
using TrackingCodeApi.repos.tag;

using TrackingCodeApi.models;
using TrackingCodeApi.dtos.auditoria;
using TrackingCodeApi.dtos.localizacao;
using TrackingCodeApi.dtos.moto;
using TrackingCodeApi.dtos.setor;
using TrackingCodeApi.dtos.tag;
using TrackingCodeApi.dtos.common;
using TrackingCodeApi.services;

namespace TrackingCodeAPI.configs
{
    public static class ServicesConfigurator
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            // ------------------ AutoMapper Profiles ------------------
            services.AddAutoMapper(typeof(AuditoriaProfile));
            services.AddAutoMapper(typeof(LocalizacaoProfile));
            services.AddAutoMapper(typeof(MotoProfile));
            services.AddAutoMapper(typeof(PagedResponseProfile));
            services.AddAutoMapper(typeof(TagProfile));
            services.AddAutoMapper(typeof(SetorProfile));

            // ------------------ Repository DI ------------------
            services.AddScoped<IAuditoriaRepository, AuditoriaRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ISetorRepository, SetorRepository>();
            services.AddScoped<IMotoRepository, MotoRepository>();
            services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();

            // ------------------ Services ------------------
            services.AddHttpContextAccessor();
            services.AddScoped<ILinkService, LinkService>();

            // ------------------ JSON Config ------------------
            services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            // ------------------ Database ------------------
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    services.AddDbContext<TrackingCodeDb>(opt =>
        opt.UseSqlServer(connectionString));

            // ------------------ Swagger / OpenAPI ------------------
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tracking Code API",
                    Version = "v1",
                    Description = @"
API para rastreamento e auditoria de motos, localizações e usuários no sistema Tracking Code.

**Recursos disponíveis:**
- 📍 Localização (rastreamento de posição)
- 🏍️ Moto (gerenciamento e vinculação a tags)
- 🏷️ Tag (identificação via QR Code ou RFID)
- 👤 Usuário (autenticação e papéis)
- 🧭 Setor (agrupamento lógico)
- 🧾 Auditoria (logs automáticos de ações)

**Principais recursos técnicos:**
- ✅ API RESTful com HATEOAS
- ✅ Paginação configurável
- ✅ Validação com FluentValidation
- ✅ Automapper para mapeamento de DTOs
- ✅ Integração com Oracle Database
"
                });

                // Define tags para organização no Swagger
                options.DocumentFilter<SwaggerTagDescriptions>();
            });
        }
    }

    // ------------------ Classe auxiliar opcional para descrever Tags ------------------
    public class SwaggerTagDescriptions : Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, Swashbuckle.AspNetCore.SwaggerGen.DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<OpenApiTag>
            {
                new OpenApiTag { Name = "Auditoria", Description = "Operações de auditoria e histórico de alterações." },
                new OpenApiTag { Name = "Localização", Description = "Gerenciamento das coordenadas de motos e sensores." },
                new OpenApiTag { Name = "Moto", Description = "CRUD de motos disponíveis no pátio ou em circulação." },
                new OpenApiTag { Name = "Setor", Description = "Agrupamento de motos, sensores e tags por setor." },
                new OpenApiTag { Name = "Tag", Description = "Gerenciamento de etiquetas de identificação (RFID/QR Code)." },
                new OpenApiTag { Name = "Usuário", Description = "Autenticação, perfis e controle de acesso." }
            };
        }
    }
}
