using Microsoft.EntityFrameworkCore;
using PassagensAreas.Domain.Models;

namespace PassagensAreas.Infraestrutura
{
    public class CheckInContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<CheckIn> CheckInSet { get; set; }

        public CheckInContext(IConfiguration configuration, DbContextOptions<CheckInContext> options)
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
