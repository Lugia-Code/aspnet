namespace TrackingCodeApi.dtos.moto
{
    public record MotoDto(
        string Chassi,
        string? Placa,
        string Modelo,
        DateTime? DataCadastro,
        string? Setor,
        int IdSetor,
        int? IdAudit,
        int CodigoTag 
    );
}