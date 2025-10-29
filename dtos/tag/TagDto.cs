namespace TrackingCodeApi.dtos.tag;

public record TagDto(
    string CodigoTag,
    string Status,
    int Bateria,
    DateTime DataVinculo,
    string? Chassi
    
);