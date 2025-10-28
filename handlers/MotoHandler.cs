using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using AutoMapper;
using TrackingCodeApi.repos.moto;
using TrackingCodeApi.repos.setor;
using TrackingCodeApi.repos.tag;
using TrackingCodeApi.models;
using TrackingCodeApi.dtos.moto;

namespace TrackingCodeApi.handlers;

public static class MotoHandler
{
    public static void MapMotoEndpoints(this IEndpointRouteBuilder app)
    {
        var motoGroup = app.MapGroup("/api/v1/motos")
            .WithTags("Motos")
            .WithOpenApi();

        //  GET /api/v1/motos
        motoGroup.MapGet("/", async (
            IMotoRepository motoRepo,
            ISetorRepository setorRepo,
            ITagRepository tagRepo,
            IMapper mapper,
            int page = 1,
            int pageSize = 10) =>
        {
            var motos = await motoRepo.GetPagedAsync(page, pageSize);

            var result = motos.Select(m => new
            {
                chassi = m.Chassi,
                placa = m.Placa,
                modelo = m.Modelo,
                dataCadastro = m.DataCadastro,
                setor = m.Setor != null ? new { m.Setor.IdSetor, m.Setor.Nome } : null,
                tag = m.Tag != null ? new { m.Tag.CodigoTag, m.Tag.Status, m.Tag.DataVinculo } : null
            });

            var totalCount = await motoRepo.CountAsync();

            return Results.Ok(new
            {
                data = result,
                pagination = new
                {
                    page,
                    pageSize,
                    totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            });
        })
        .WithSummary("Lista todas as motos com paginação")
        .WithDescription("Retorna todas as motos cadastradas, com informações de setor e tag.")
        .WithOpenApi();

        //  GET /api/v1/motos/{chassi}
        motoGroup.MapGet("/{chassi}", async (string chassi, IMotoRepository motoRepo, IMapper mapper) =>
        {
            var moto = await motoRepo.GetByChassiAsync(chassi);
            if (moto == null)
                return Results.NotFound(new { erro = "Moto não encontrada" });

            var dto = mapper.Map<MotoDto>(moto);
            return Results.Ok(dto);
        })
        .WithSummary("Busca moto por chassi")
        .WithDescription("Retorna detalhes completos de uma moto pelo chassi.")
        .WithOpenApi();

        //  POST /api/v1/motos
        motoGroup.MapPost("/", async (MotoDto dto, IMotoRepository motoRepo, ITagRepository tagRepo, ISetorRepository setorRepo, IMapper mapper) =>
        {
            // Validação de existência de setor e tag
            var setor = await setorRepo.GetByIdAsync(dto.IdSetor);
            if (setor == null)
                return Results.BadRequest(new { erro = "Setor não encontrado", campo = "idSetor" });

            var tag = await tagRepo.GetByCodigoAsync(dto.CodigoTag);
            if (tag == null || !tag.EstaDisponivel)
                return Results.BadRequest(new { erro = "Tag inválida ou indisponível", campo = "codigoTag" });

            // Criação da moto
            var moto = mapper.Map<Moto>(dto);
            moto.DataCadastro = DateTime.Now;

            await motoRepo.AddAsync(moto);
            await motoRepo.SaveAsync();

            return Results.Created($"/api/v1/motos/{moto.Chassi}", mapper.Map<MotoDto>(moto));
        })
        .WithSummary("Cadastra uma nova moto")
        .WithDescription("Cria uma nova motocicleta vinculando tag e setor existentes.")
        .WithOpenApi();
    }
}
