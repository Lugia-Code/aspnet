namespace TrackingCodeApi.dtos.usuario;

public record UsuarioDto(
    int IdFuncionario,
    string Nome,
    string Email,
    string Funcao
);