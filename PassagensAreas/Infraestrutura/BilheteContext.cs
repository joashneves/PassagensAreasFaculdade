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
            var connectionString = _configuration.GetConnectionString("MySQLData");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
