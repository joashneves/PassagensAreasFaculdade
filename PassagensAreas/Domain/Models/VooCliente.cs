using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PassagensAreas.Domain.Models
{
    public class VooCliente
    {
        [Key] // Define o campo como chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Gera o valor automaticamente
        public int Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public DateTime Datas { get; set; }
        public DateTime Ida { get; set; }
        public DateTime? Volta { get; set; }
        public float Preco { get; set; }
        public string Companhias { get; set; }
        public int QuantidadeMaximaPassageiros {  get; set; }
        public int QuantidadePassageiros { get; set; }
        public int NumeroVoo {  get; set; }

    }
}
