using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TrackingCodeApi.repos.setor;
using TrackingCodeApi.repos.localizacao;
using AutoMapper;
using TrackingCodeApi.dtos.setor;
using TrackingCodeApi.models;

namespace TrackingCodeApi.handlers
{
    public static class SetorHandler
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1/setores")
                .WithTags("Setores")
                .WithOpenApi();

            // GET paginado
            group.MapGet("/", async (ISetorRepository repo, IMapper mapper, int page = 1, int pageSize = 10) =>
            {
                var setores = await repo.GetPagedAsync(page, pageSize);
                var dtos = mapper.Map<IEnumerable<SetorDto>>(setores);
                return Results.Ok(dtos);
            })
            .WithSummary("Lista todos os setores (paginado)");

            // GET por ID
            group.MapGet("/{id:int}", async (int id, ISetorRepository repo, IMapper mapper) =>
            {
                var setor = await repo.GetByIdAsync(id);
                if (setor == null)
                    return Results.NotFound(new { erro = "Setor não encontrado" });

                return Results.Ok(mapper.Map<SetorDto>(setor));
            })
            .WithSummary("Busca um setor pelo ID");

            // POST - criação
            group.MapPost("/", async (SetorDto dto, ISetorRepository repo, IMapper mapper) =>
            {
                var setor = mapper.Map<Setor>(dto);
                await repo.CreateAsync(setor);
                return Results.Created($"/api/v1/setores/{setor.IdSetor}", mapper.Map<SetorDto>(setor));
            })
            .WithSummary("Cria um novo setor");

            // DELETE - Exclusão
            group.MapDelete("/{id:int}", async (int id, ISetorRepository repo, ILocalizacaoRepository localizacaoRepo) =>
            {
                var setor = await repo.GetByIdAsync(id);
                if (setor == null)
                    return Results.NotFound(new { erro = "Setor não encontrado" });

                // Passo 1: Atualizar ou excluir as localizações associadas a este setor
                var localizacoes = await localizacaoRepo.GetBySetorIdAsync(id);  // Alterado para buscar todas as localizações do setor
                foreach (var localizacao in localizacoes)
                {
                    // Remover ou atualizar as localizações. Aqui estamos apenas desvinculando.
                    localizacao.IdSetor = 0;  // Desvincula a localização do setor
                    await localizacaoRepo.UpdateAsync(localizacao);  // Atualiza a localização para desvincular o setor
                }

                try
                {
                    // Passo 2: Deletar o setor
                    await repo.DeleteAsync(setor);  // Deleta o setor após desvincular as localizações
                    return Results.NoContent();  // Retorna 204 No Content em caso de sucesso
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro ao excluir setor: {ex.Message}");
                }
            })
            .WithSummary("Deleta um setor pelo ID");
        }
    }
}
