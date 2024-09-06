﻿namespace PassagensAreas.Domain.Models
{
    public class CheckIn
    {
        public int Id { get; set; }
        public int IdReserva { get; set; } 
        public DateTime DataCheckIn { get; set; }  
        public string AssentoEscolhido { get; set; } 
        public bool CheckInRealizado { get; set; } 
    }
}
