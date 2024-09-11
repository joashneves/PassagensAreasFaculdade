namespace PassagensAreas.Domain.DTOs
{
    public class BilheteDTO
    {
        public string NomeCliente { get; set; }
        public int NumeroVoo { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public DateTime DataVoo { get; set; }
        public string Assento { get; set; }
        public DateTime DataEmissao { get; set; }
    }
}
