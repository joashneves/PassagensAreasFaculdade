namespace PassagensAreas.Domain.Models
{
    public class ReservaDePassagem
    {
        public int Id { get; set; }
        public int Id_voo { get; set; }
        public DateOnly DataReserva {  get; set; }
        public int NumeroVoo { get; set; }
        public int AssentosOcupados { get; set; }
    }
}
