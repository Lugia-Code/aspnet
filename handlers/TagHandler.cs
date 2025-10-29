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

            // GET paginado
            group.MapGet("/", async (ITagRepository repo, IMapper mapper, int page = 1, int pageSize = 10) =>
                {
                    var tags = await repo.GetPagedAsync(page, pageSize);
                    var dtos = mapper.Map<IEnumerable<TagDto>>(tags);
                    return Results.Ok(dtos);
                })
                .WithSummary("Lista todas as tags (paginado)");

            // GET por código
            group.MapGet("/{codigo}", async (string codigo, ITagRepository repo, IMapper mapper) =>
                {
                    var tag = await repo.GetByCodigoAsync(codigo);
                    if (tag == null)
                        return Results.NotFound(new { erro = "Tag não encontrada" });

                    return Results.Ok(mapper.Map<TagDto>(tag));
                })
                .WithSummary("Busca uma tag pelo código");

            // POST - criação com validação de chassi
            group.MapPost("/", async (TagDto dto, ITagRepository repo, IMapper mapper) =>
                {
                    try
                    {
                        var tag = mapper.Map<Tag>(dto);

                        // Valida se já existe outra tag com o mesmo chassi
                        if (!string.IsNullOrWhiteSpace(tag.Chassi))
                        {
                            var chassiExistente = await repo.AnyWithChassiAsync(tag.Chassi);
                            if (chassiExistente)
                                return Results.BadRequest(new { erro = $"Chassi {tag.Chassi} já está vinculado a outra tag." });
                        }

                        // Cria a tag
                        await repo.CreateAsync(tag);
                        return Results.Created($"/api/v1/tags/{tag.CodigoTag}", mapper.Map<TagDto>(tag));
                    }
                    catch (Exception ex)
                    {
                        return Results.Problem(ex.Message);
                    }
                })
                .WithSummary("Cria uma nova tag");
        }
    }
}
