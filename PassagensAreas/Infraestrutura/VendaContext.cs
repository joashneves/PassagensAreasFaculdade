using Microsoft.EntityFrameworkCore;
using PassagensAreas.Domain.Models;

namespace PassagensAreas.Infraestrutura
{
    public class VendaContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<RelatorioVendas> VendaSet { get; set; }

        public VendaContext(IConfiguration configuration, DbContextOptions<VendaContext> options)
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
