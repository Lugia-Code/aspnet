using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using TrackingCodeApi.repos.moto;
using TrackingCodeApi.repos.tag;
using TrackingCodeApi.repos.setor;
using TrackingCodeApi.models;
using AutoMapper;
using TrackingCodeApi.dtos.moto;

namespace TrackingCodeApi.handlers
{
    public static class MotoHandler
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1/motos")
                .WithTags("Motos")
                .WithOpenApi();

            // GET paginado
            group.MapGet("/", async (IMotoRepository motoRepo, IMapper mapper, int page = 1, int pageSize = 10) =>
            {
                var motos = await motoRepo.GetPagedAsync(page, pageSize);
                var dtos = mapper.Map<IEnumerable<MotoDto>>(motos);
                return Results.Ok(dtos);
            })
            .WithSummary("Lista todas as motos (paginado)")
            .WithDescription("Retorna uma lista de motos cadastradas no sistema, com suporte a paginação.");

            // GET por ID
            group.MapGet("/{id}", async (string id, IMotoRepository motoRepo, IMapper mapper) =>
            {
                var moto = await motoRepo.FindAsyncById(id);
                if (moto == null)
                    return Results.NotFound(new { erro = "Moto não encontrada" });

                return Results.Ok(mapper.Map<MotoDto>(moto));
            })
            .WithSummary("Busca uma moto pelo ID (chassi)");

            // 🔹 POST - Criação
            group.MapPost("/", async (
                MotoDto dto,
                IMotoRepository motoRepo,
                ITagRepository tagRepo,
                ISetorRepository setorRepo,
                IMapper mapper,
                TrackingCodeDb db) =>
            {
                // Validação de Setor
                var setor = await setorRepo.GetByIdAsync(dto.IdSetor);
                if (setor == null)
                    return Results.BadRequest(new { erro = "Setor não encontrado", campo = "idSetor" });

                // Validação de Tag
                var tag = await tagRepo.GetByCodigoAsync(dto.CodigoTag);
                if (tag == null || !tag.EstaDisponivel)
                    return Results.BadRequest(new { erro = "Tag inválida ou indisponível", campo = "codigoTag" });

                using var transaction = await db.Database.BeginTransactionAsync();
                try
                {
                    var moto = mapper.Map<Moto>(dto);
                    moto.DataCadastro = DateTime.Now;

                    await motoRepo.AddAsync(moto);

                    // Marca a tag como não disponível
                    tag.Status = "Inativo";
                    await tagRepo.UpdateAsync(tag);

                    await transaction.CommitAsync();
                    return Results.Created($"/api/v1/motos/{moto.Chassi}", mapper.Map<MotoDto>(moto));
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return Results.Problem("Erro ao cadastrar moto.");
                }
            })
            .WithSummary("Cadastra uma nova moto")
            .WithDescription("Cria uma moto e vincula uma tag e setor, validando disponibilidade e consistência.");
        }
    }
}
