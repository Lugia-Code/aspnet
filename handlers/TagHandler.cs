using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TrackingCodeApi.repos.tag;
using AutoMapper;
using TrackingCodeApi.dtos.tag;
using TrackingCodeApi.models;

namespace TrackingCodeApi.handlers
{
    public static class TagHandler
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1/tags")
                .WithTags("Tags")
                .WithOpenApi();

            // 🔹 GET paginado
            group.MapGet("/", async (ITagRepository repo, IMapper mapper, int page = 1, int pageSize = 10) =>
                {
                    var tags = await repo.GetPagedAsync(page, pageSize);
                    var dtos = mapper.Map<IEnumerable<TagDto>>(tags);
                    return Results.Ok(dtos);
                })
                .WithSummary("Lista todas as tags (paginado)");

            // 🔹 GET por código
            group.MapGet("/{codigo:int}", async (int codigo, ITagRepository repo, IMapper mapper) =>
                {
                    var tag = await repo.GetByCodigoAsync(codigo);
                    if (tag == null)
                        return Results.NotFound(new { erro = "Tag não encontrada" });

                    return Results.Ok(mapper.Map<TagDto>(tag));
                })
                .WithSummary("Busca uma tag pelo código");

            // 🔹 POST - criação
            group.MapPost("/", async (TagDto dto, ITagRepository repo, IMapper mapper) =>
                {
                    var tag = mapper.Map<Tag>(dto);
                    await repo.CreateAsync(tag);
                    return Results.Created($"/api/v1/tags/{tag.CodigoTag}", mapper.Map<TagDto>(tag));
                })
                .WithSummary("Cria uma nova tag");
        }
    }
}