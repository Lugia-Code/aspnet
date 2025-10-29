using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TrackingCodeApi.repos.usuario;
using TrackingCodeApi.dtos.usuario;
using AutoMapper;
using TrackingCodeApi.models;

namespace TrackingCodeApi.handlers
{
    public static class UsuarioHandler
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1/usuarios")
                .WithTags("Usuários")
                .WithOpenApi();

            //  GET todos
            group.MapGet("/", async (IUsuarioRepository repo, IMapper mapper) =>
            {
                var usuarios = await repo.GetAllAsync();
                var dtos = mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
                return Results.Ok(dtos);
            })
            .WithSummary("Lista todos os usuários");

            //  GET por ID
            group.MapGet("/{id:int}", async (int id, IUsuarioRepository repo, IMapper mapper) =>
            {
                var usuario = await repo.GetByIdAsync(id);
                if (usuario == null)
                    return Results.NotFound(new { erro = "Usuário não encontrado" });

                return Results.Ok(mapper.Map<UsuarioDto>(usuario));
            })
            .WithSummary("Busca um usuário pelo ID");

            //  POST - criação
            group.MapPost("/", async (UsuarioDto dto, IUsuarioRepository repo, IMapper mapper) =>
            {
                var usuario = mapper.Map<Usuario>(dto);
                await repo.CreateAsync(usuario);
                return Results.Created($"/api/v1/usuarios/{usuario.IdFuncionario}", mapper.Map<UsuarioDto>(usuario));
            })
            .WithSummary("Cria um novo usuário");
        }
    }
}
