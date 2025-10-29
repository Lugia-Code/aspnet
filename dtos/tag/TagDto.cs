namespace TrackingCodeApi.dtos.tag;

public record TagDto
{
    public string CodigoTag { get; set; } = string.Empty;
    public string Status { get; set; } = "inativo";
    public int Bateria { get; set; } = 0; 
    public DateTime DataVinculo { get; set; } = DateTime.Now;
    public string? Chassi { get; set; }
}