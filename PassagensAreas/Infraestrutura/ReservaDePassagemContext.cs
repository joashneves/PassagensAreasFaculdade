using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using PassagensAreas.Domain.Models;

namespace PassagensAreas.Infraestrutura
{
    public class ReservaDePassagemContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<ReservaDePassagem> ReservaDePassagemSet { get; set; }

        public ReservaDePassagemContext(IConfiguration configuration, DbContextOptions<ReservaDePassagemContext> options)
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
