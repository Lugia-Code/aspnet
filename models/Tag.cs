using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TrackingCodeApi.models
{
    [Table("TAG")]
    public class Tag
    {
        [Key]
        [Column("CODIGO_TAG")]
        public required string CodigoTag { get; set; } = String.Empty;

        [Required]
        [Column("BATERIA")]
        public int Bateria { get; set; } = 0; // Valor padrão para evitar null

        [Required]
        [Column("STATUS")]
        public string Status { get; set; } = "inativo"; // Status inicial da tag é inativo

        [Required]
        [Column("DATA_VINCULO")]
        public DateTime DataVinculo { get; set; } = DateTime.Now;

        // Chave estrangeira para Moto (agora, a TAG tem o Chassi da Moto como referência)
        [Column("CHASSI")]
        public string? Chassi { get; set; }

        // Propriedade de navegação
        [JsonIgnore]
        public virtual Moto? Moto { get; set; } // A Tag está associada a uma Moto

        // Relacionamento um-para-muitos com Localizacao
        [JsonIgnore]
        public virtual ICollection<Localizacao> Localizacoes { get; set; } = new List<Localizacao>();

        // Regra de negócio: ao vincular moto -> ativa a tag
        public void VincularMoto(string chassi)
        {
            if (string.IsNullOrWhiteSpace(chassi))
                throw new ArgumentException("Chassi não pode ser nulo ou vazio", nameof(chassi));

            if (!string.IsNullOrEmpty(Chassi) && Chassi != chassi)
                throw new InvalidOperationException($"Tag já está vinculada à moto com chassi: {Chassi}");

            Chassi = chassi;
            Status = "ativo"; // Marca a tag como ativa
            DataVinculo = DateTime.Now; // Define a data de vínculo da tag com a moto
        }

        // Método para desvincular uma moto da tag (desativa a tag)
        public void DesvincularMoto()
        {
            Chassi = null; // Remove o vínculo com a moto
            Status = "inativo"; // Marca a tag como inativa
            // Mantém a DataVinculo para histórico, mas não altera
        }

        // Propriedade para verificar se a tag está disponível
        [JsonIgnore]
        public bool EstaDisponivel => string.IsNullOrEmpty(Chassi) || Status == "inativo"; // A tag está disponível se não tiver um chassi vinculado ou se estiver inativa

        // Propriedade para verificar se a tag está ativa
        [JsonIgnore]
        public bool EstaAtiva => Status == "ativo" && !string.IsNullOrEmpty(Chassi); // A tag está ativa se o status for ativo e o chassi estiver vinculado
    }
}
