using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingCodeApi.models;

[Table("USUARIO")]
public class Usuario
{
    [Key]
    public int IdFuncionario { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;   // inicialização remove o warning

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;

    [Required]
    public string Funcao { get; set; } = string.Empty;

    public virtual ICollection<Auditoria> Auditorias { get; set; } = new List<Auditoria>();
}