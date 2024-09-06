using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassagensAreas.Domain.Models
{
    public class Cliente
    {
        [Key] // Define o campo como chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Gera o valor automaticamente
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string Numero_telefone { get; set; }
        public string Numero_telefone_fixo { get; set; }
        public string Email { get; set; }

    }
}