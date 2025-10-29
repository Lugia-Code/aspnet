using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TrackingCodeApi.models;

[Table("MOTO")]
public class Moto
{
    [Key]
    [Column("CHASSI", TypeName = "CHAR(17)")] 
    public string Chassi { get; set; } = string.Empty;

    [Column("PLACA", TypeName = "CHAR(7)")]
    public string? Placa { get; set; }

    [Required]
    [Column("MODELO", TypeName = "VARCHAR2(30)")]
    public string Modelo { get; set; } = string.Empty;

    [Required]
    [Column("DATA_CADASTRO")]
    public DateTime DataCadastro { get; set; } = DateTime.Now;

    [Required]
    [Column("ID_SETOR")]
    public int IdSetor { get; set; }

    [Column("ID_AUDIT")]
    public int? IdAudit { get; set; }

    // 🔗 Relacionamentos
    [JsonIgnore]
    [ForeignKey("IdSetor")]
    public virtual Setor Setor { get; set; }
    


    [JsonIgnore]
    [ForeignKey("IdAudit")]
    public virtual Auditoria? Auditoria { get; set; }


}



