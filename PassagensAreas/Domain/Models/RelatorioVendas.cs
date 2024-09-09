namespace PassagensAreas.Domain.Models
{
    public class RelatorioVendas
    {
        public int Id { get; set; }
        public string NumeroVoo { get; set; }
        public decimal ValorTotal { get; set; }
        public string FormaPagamento { get; set; }
       
    }
}
