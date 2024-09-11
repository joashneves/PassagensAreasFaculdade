using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassagensAreas.Domain.DTOs
{
    public class ClienteDTO
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string Numero_telefone { get; set; }
        public string Numero_telefone_fixo { get; set; }
        public string Email { get; set; }

    }
}