namespace TrackingCodeApi.dtos.moto;

public record MotoDto
{
    public string Chassi { get; set; } = string.Empty;
    public string? Placa { get; set; }
    public string Modelo { get; set; } = string.Empty;
    public DateTime? DataCadastro { get; set; } = DateTime.Now;
    public string? Setor { get; set; }
    public int IdSetor { get; set; }
    public int? IdAudit { get; set; }

}