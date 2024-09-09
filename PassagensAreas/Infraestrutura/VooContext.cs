using Microsoft.EntityFrameworkCore;
using PassagensAreas.Domain.Models;
using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infraestrutura
{
    public class VooContext : DbContext
    {
        private IConfiguration _configuration;
        internal readonly object ReservaDePassagemSet;

        public DbSet<VooCliente> VooSet { get; set; }

        public VooContext(IConfiguration configuration, DbContextOptions<VooContext> options)
            : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("MySQLData");
            optionsBuilder.UseMySQL(connectionString);
        }
        public void InitializeData()
        {
            // Verifica se já existem registros na tabela
            if (VooSet.Any())
            {
                // Se houver registros, não faz nada
                return;
            }
            // Caminho para o arquivo JSON
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "VooInicial.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Arquivo JSON não encontrado: {filePath}");
            }

            var jsonData = File.ReadAllText(filePath);
            var voos = JsonConvert.DeserializeObject<List<VooCliente>>(jsonData);

            if (voos != null)
            {
                VooSet.AddRange(voos); // Adiciona os voos no DbSet
                SaveChanges(); // Salva as alterações no banco em memória
            }
        }
    }
}
