namespace PassagensAreas.Domain.Models
{
    public class RelatorioOcupacao
    {
        public int Id { get; set; }
        public int VooId { get; set; }
        public DateTime DataRelatorio { get; set; }
        public double PercentualOcupacao { get; set; }
    }
}
