using Microsoft.EntityFrameworkCore;
using PassagensAreas.Domain.Models;


namespace PassagensAreas.Infraestrutura
{
    public class AssentoContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<Assento> AssentoSet { get; set; }

        public AssentoContext(IConfiguration configuration, DbContextOptions<AssentoContext> options)
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
