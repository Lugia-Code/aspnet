namespace TrackingCodeApi.dtos.usuario;

public record UsuarioDto
{
    public int IdFuncionario { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Funcao { get; set; } = string.Empty;
}