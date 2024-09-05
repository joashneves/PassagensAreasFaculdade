using Microsoft.EntityFrameworkCore;
using PassagensAreas.Domain.Models;
using System;
using Microsoft.Extensions.Configuration;

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
            // Se desejar usar o MySQL no futuro, deixe isso comentado
            // var connectionString = _configuration.GetConnectionString("ClienteData");
            // optionsBuilder.UseMySQL(connectionString);

            // Configuração temporária para usar banco de dados em memória
            optionsBuilder.UseInMemoryDatabase("VooDataInMemory");
        }
    }
}
