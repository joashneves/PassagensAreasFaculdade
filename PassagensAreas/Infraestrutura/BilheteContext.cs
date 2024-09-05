using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using PassagensAreas.Domain.Models;

namespace PassagensAreas.Infraestrutura
{
    public class BilheteContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<Bilhete> BilheteSet { get; set; }

        public BilheteContext(IConfiguration configuration, DbContextOptions<BilheteContext> options)
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
            optionsBuilder.UseInMemoryDatabase("BilheteDataInMemory");
        }
    }
}
