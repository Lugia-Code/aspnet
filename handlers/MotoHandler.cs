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

            //  GET por chassi
            group.MapGet("/{id}", async (string id, IMotoRepository motoRepo, IMapper mapper) =>
            {
                var moto = await motoRepo.FindAsyncById(id);
                if (moto == null)
                    return Results.NotFound(new { erro = "Moto não encontrada" });

                return Results.Ok(mapper.Map<MotoDto>(moto));
            })
            .WithSummary("Busca uma moto pelo ID (chassi)");
            
            //  GET - Listar motos por setor
            group.MapGet("/setor/{idSetor}", async (
                    int idSetor,
                    IMotoRepository motoRepo,
                    IMapper mapper,
                    ISetorRepository setorRepo) =>
                {
                    var setor = await setorRepo.GetByIdAsync(idSetor);
                    if (setor == null)
                        return Results.NotFound(new { erro = "Setor não encontrado" });

                    var motos = await motoRepo.GetBySetorAsync(idSetor); 
                    var dtos = mapper.Map<IEnumerable<MotoDto>>(motos);
                    return Results.Ok(dtos);
                })
                .WithSummary("Lista motos de um setor específico")
                .WithDescription("Retorna todas as motos que pertencem a determinado setor.");


            //  PATCH - Desvincular Tag de uma Moto
            group.MapPatch("/{chassi}/desvincular-tag", async (
                string chassi,
                IMotoRepository motoRepo,
                ITagRepository tagRepo,
                TrackingCodeDb db) =>
            {
                var moto = await motoRepo.FindAsyncById(chassi);
                if (moto == null)
                    return Results.NotFound(new { erro = "Moto não encontrada" });

                // Verificar se existe tag vinculada a essa moto
                bool existeTag = await tagRepo.AnyWithChassiAsync(chassi);
                if (!existeTag)
                    return Results.BadRequest(new { erro = "Nenhuma tag está vinculada a esta moto." });

                using var transaction = await db.Database.BeginTransactionAsync();
                try
                {
                    // Buscar e atualizar a tag vinculada
                    var tag = await db.Tag.FirstOrDefaultAsync(t => t.Chassi == chassi);
                    if (tag != null)
                    {
                        tag.Chassi = null;
                        tag.DataVinculo = tag.DataVinculo;
                        tag.Status = "inativo"; 
                        await tagRepo.UpdateAsync(tag);
                    }

                    await transaction.CommitAsync();
                    return Results.Ok(new { mensagem = "Tag desvinculada com sucesso." });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Results.Problem($"Erro ao desvincular tag: {ex.Message}");
                }
            })
            .WithSummary("Desvincula a tag de uma moto")
            .WithDescription("Remove a associação da tag com a moto e a marca como INATIVA.");

            //  DELETE - Excluir Moto
            group.MapDelete("/{chassi}", async (
                string chassi,
                IMotoRepository motoRepo,
                ITagRepository tagRepo,
                TrackingCodeDb db) =>
            {
                var moto = await motoRepo.FindAsyncById(chassi);
                if (moto == null)
                    return Results.NotFound(new { erro = "Moto não encontrada" });

                // Impedir exclusão se a moto ainda tiver uma tag vinculada
                bool possuiTag = await tagRepo.AnyWithChassiAsync(chassi);
                if (possuiTag)
                    return Results.BadRequest(new { erro = "A moto ainda possui uma tag vinculada. Desvincule antes de excluir." });

                using var transaction = await db.Database.BeginTransactionAsync();
                try
                {
                    await motoRepo.DeleteAsync(moto);
                    await transaction.CommitAsync();

                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Results.Problem($"Erro ao deletar moto: {ex.Message}");
                }
            })
            .WithSummary("Remove uma moto")
            .WithDescription("Exclui uma moto do sistema, apenas se ela não possuir tag vinculada.");
            
            // PATCH - Trocar setor da moto
            group.MapPatch("/{chassi}/trocar-setor/{novoIdSetor}", async (
                    string chassi,
                    int novoIdSetor,
                    IMotoRepository motoRepo,
                    ISetorRepository setorRepo,
                    TrackingCodeDb db) =>
                {
                    var moto = await motoRepo.FindAsyncById(chassi);
                    if (moto == null)
                        return Results.NotFound(new { erro = "Moto não encontrada" });

                    var setor = await setorRepo.GetByIdAsync(novoIdSetor);
                    if (setor == null)
                        return Results.NotFound(new { erro = "Setor não encontrado" });

                    using var transaction = await db.Database.BeginTransactionAsync();
                    try
                    {
                        moto.IdSetor = novoIdSetor;
                        await motoRepo.UpdateAsync(moto);

                        await transaction.CommitAsync();
                        return Results.Ok(new { mensagem = $"Moto {chassi} movida para o setor {novoIdSetor}" });
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return Results.Problem($"Erro ao trocar setor da moto: {ex.Message}");
                    }
                })
                .WithSummary("Troca o setor de uma moto")
                .WithDescription("Altera o setor de uma moto existente para outro setor válido.");


            //  POST - Criação
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
                var tag = await tagRepo.GetByCodigoAsync(dto.Chassi);
                if (tag == null || !tag.EstaDisponivel)
                    return Results.BadRequest(new { erro = "Tag inválida ou indisponível", campo = "Chassi" });

                using var transaction = await db.Database.BeginTransactionAsync();
                try
                {
                    var moto = mapper.Map<Moto>(dto);
                    moto.DataCadastro = DateTime.Now;

                    await motoRepo.AddAsync(moto);

                    // Marca a tag como ativa
                    tag.Status = "ativa"; 
                    tag.Chassi = moto.Chassi;
                    tag.DataVinculo = DateTime.Now;
                    await tagRepo.UpdateAsync(tag);

                    await transaction.CommitAsync();
                    return Results.Created($"/api/v1/motos/{moto.Chassi}", mapper.Map<MotoDto>(moto));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Results.Problem($"Erro ao cadastrar moto: {ex.Message}");
                }
            })
            .WithSummary("Cadastra uma nova moto")
            .WithDescription("Cria uma moto e vincula uma tag e setor, validando disponibilidade e consistência.");
        }
    }
}
