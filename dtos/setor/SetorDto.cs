namespace TrackingCodeApi.dtos.setor;

public record SetorDto(
    int IdSetor,
    string Nome,
    string? Descricao,
    string? CoordenadasLimite
);