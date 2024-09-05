using Microsoft.EntityFrameworkCore;
using PassagensAreas.Domain.Models;
using System;
using Microsoft.Extensions.Configuration;

namespace Infraestrutura
{
    public class ClienteContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<Cliente> ClienteSet { get; set; }

        public ClienteContext(IConfiguration configuration, DbContextOptions<ClienteContext> options)
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
            optionsBuilder.UseInMemoryDatabase("ClienteDataInMemory");
        }
    }
}
