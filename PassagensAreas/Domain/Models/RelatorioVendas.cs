namespace PassagensAreas.Domain.Models
{
    public class RelatorioVendas
    {
        public int NumeroVoo { get; set; }
        public float TotalArrecadado { get; set; }
        public float TotalMesAnterior { get; set; }
        public float ComparacaoComMesAnterior { get; set; }
        public string Periodo { get; set; }
        public List<Pagamento> PagamentosPorForma { get; set; }
        public DateTime DataVenda { get; internal set; }
        public int ValorPago { get; internal set; }
    }
}
