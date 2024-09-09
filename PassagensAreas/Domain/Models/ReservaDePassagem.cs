namespace PassagensAreas.Domain.Models
{
    public class ReservaDePassagem
    {
        public int Id { get; set; }
        public int Id_Voo { get; set; }
        public string CPFCliente { get; set; }
        public DateTime DataReserva {  get; set; }
        public int NumeroVoo { get; set; }
        public int AssentosReservados { get; set; }
        public string FormaPagamento { get; set; } 
        public decimal ValorTotal { get; set; } 

    }
}
