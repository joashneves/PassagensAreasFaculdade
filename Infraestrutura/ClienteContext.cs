using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infraestrutura
{
    public class ClienteContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<Cliente> AditivoSet { get; set; }
        public AditivoContext(IConfiguration configuration, DbContextOptions<AditivoContext> options) : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("ObrasData");
            optionsBuilder.UseMySQL(connectionString);
        }
    }
}
