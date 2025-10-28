namespace TrackingCodeApi.dtos.localizacao
{
    public class LocalizacaoDto
    {
        public int IdLocalizacao { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public string CodigoTag { get; set; }
        public int IdSetor { get; set; }
    }
}