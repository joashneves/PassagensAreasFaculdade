using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassagensAreas.Domain.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public int Numero_telefone { get; set; }
        public int Numero_telefone_fixo { get; set; }
        public string Email { get; set; }

    }
}