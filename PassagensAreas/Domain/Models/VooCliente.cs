namespace PassagensAreas.Domain.Models
{
    public class VooCliente
    {
        public int Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public DateOnly Datas { get; set; }
        public DateOnly Ida { get; set; }
        public DateOnly Volta { get; set; }
        public float Preco { get; set; }
        public string Companhias { get; set; }
        public int QuantidadeMaximaPassageiros {  get; set; }
        public int QuantidadePassageiros { get; set; }
        public int NumeroVoo {  get; set; }

    }
}
