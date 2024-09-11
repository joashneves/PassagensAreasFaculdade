namespace PassagensAreas.Domain.DTOs
{
    public class ReservaDePassagemDTO
    {
        public string CPFCliente { get; set; }
        public DateTime DataReserva {  get; set; }
        public int NumeroVoo { get; set; }
        public int AssentosReservados { get; set; }
        public string FormaPagamento { get; set; } 
        public decimal ValorTotal { get; set; } 

    }
}
