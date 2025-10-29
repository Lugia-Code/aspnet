using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingCodeApi.models;

[Table("AUDITORIA_MOVIMENTACAO")]
public class Auditoria
{
    [Key]
    [Column("ID_AUDIT")]
    public int IdAudit { get; set; }

    [Required]
    [Column("id_funcionario")]
    public int IdFuncionario { get; set; }


    [Column("tipo_operacao")]
    public required  string TipoOperacao { get; set; } = string.Empty; 

    [Required]
    [Column("data_operacao")]
    public DateTime DataOperacao { get; set; }

    [Column("valores_novos")]
    public string? ValoresNovos { get; set; }

    [Column("valores_anteriores")]
    public string? ValoresAnteriores { get; set; }

    // Propriedade de navegação
    [ForeignKey("IdFuncionario")]
    public virtual Usuario Usuario { get; set; }
}