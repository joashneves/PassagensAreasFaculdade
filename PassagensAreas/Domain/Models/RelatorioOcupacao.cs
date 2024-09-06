namespace PassagensAreas.Domain.Models
{
    public class RelatorioOcupacao
    {
        public int NumeroVoo { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public int TotalAssentos { get; set; }
        public int AssentosOcupados { get; set; }
        public float PercentualOcupacao { get; set; }
        public DateTime DataVoo { get; set; }
    }
}
