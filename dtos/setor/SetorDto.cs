namespace TrackingCodeApi.dtos.setor;

public record SetorDto
{
    public int IdSetor { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string? CoordenadasLimite { get; set; }
}