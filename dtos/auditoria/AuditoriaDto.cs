namespace TrackingCodeApi.dtos.auditoria;

public record AuditoriaDto(
    int IdAudit,
    int IdFuncionario,
    string NomeFuncionario,
    string TipoOperacao,
    DateTime DataOperacao,
    string ValoresNovos,
    string ValoresAnteriores
);
