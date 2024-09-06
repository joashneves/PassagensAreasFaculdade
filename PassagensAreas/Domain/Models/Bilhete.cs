namespace PassagensAreas.Domain.Models
{
    public class Bilhete
    {
        public int Id { get; set; }
        public int Id_ReservaDePassagem { get; set; }
        public string NomeCliente { get; set; }
        public string NumeroVoo { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public DateTime DataVoo { get; set; }
        public string Assento { get; set; }
        public DateTime DataEmissao { get; set; }
    }
}
