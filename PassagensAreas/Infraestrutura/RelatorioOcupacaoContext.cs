using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using PassagensAreas.Domain.Models;

namespace PassagensAreas.Infraestrutura
{
    public class RelatorioOcupacaoContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<RelatorioOcupacao> RelatorioOcupacaoSet { get; set; }

        public RelatorioOcupacaoContext(IConfiguration configuration, DbContextOptions<RelatorioOcupacaoContext> options)
            : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("MySQLData");
            optionsBuilder.UseMySQL(connectionString);
        }
    }
}
