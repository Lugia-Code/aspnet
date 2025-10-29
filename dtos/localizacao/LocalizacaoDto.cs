namespace TrackingCodeApi.dtos.localizacao;

public record LocalizacaoDto
{
    public int IdLocalizacao { get; set; }
    public decimal X { get; set; }
    public decimal Y { get; set; }
    public string CodigoTag { get; set; } = string.Empty;
    public int IdSetor { get; set; }
}