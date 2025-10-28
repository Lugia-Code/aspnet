using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingCodeApi.models;

[Table("localizacao")]
public class Localizacao
{
    [Key]
    [Column("id_localizacao")]
    public int IdLocalizacao { get; set; }

    [Required]
    [Column("x")]
    public decimal X { get; set; }

    [Required]
    [Column("y")]
    public decimal Y { get; set; }

    [Required]
    [Column("codigo_tag")]
    public string CodigoTag { get; set; }

    [Required]
    [Column("id_setor")]
    public int IdSetor { get; set; }

    // Propriedades de navegação
    [ForeignKey("CodigoTag")]
    public virtual Tag Tag { get; set; }

    [ForeignKey("IdSetor")]
    public virtual Setor Setor { get; set; }
}