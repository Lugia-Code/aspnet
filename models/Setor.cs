using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TrackingCodeApi.models;


[Table("SETOR")]
public class Setor
{
    [Key]
    [Column("ID_SETOR")]
    public int IdSetor { get; set; }
    
    [Required]
    [Column("NOME")]
    public string Nome { get; set; }
    
    [Column("DESCRICAO")]
    public string? Descricao { get; set; }
    
    // Corrigido o nome da coluna para "COORDENADAS_LIMITE"
    [Column("COORDENADAS_LIMITE")]
    public string? CoordenadasLimite { get; set; }
    
    [JsonIgnore]
    public ICollection<Moto> Motos { get; set; } = new List<Moto>(); // Relacionamento com a Moto
    [JsonIgnore]
    public ICollection<Localizacao> Localizacoes { get; set; } = new List<Localizacao>(); // Relacionamento com Localizacao
}
