namespace TrackingCodeApi.dtos.usuario;



public record UsuarioReadDto(
    int Id,
    string Nome,
    string Email,
    string Funcao
);


public record UsuarioCreateDto(
    string Nome,
    string Email,
    string Senha,
    string Funcao
);


public record UsuarioUpdateDto(
    string? Nome,
    string? Email,
    string? Senha,
    string? Funcao
);
