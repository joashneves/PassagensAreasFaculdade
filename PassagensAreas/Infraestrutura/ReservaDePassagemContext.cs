﻿using Infraestrutura;
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
            // Se desejar usar o MySQL no futuro, deixe isso comentado
            // var connectionString = _configuration.GetConnectionString("ClienteData");
            // optionsBuilder.UseMySQL(connectionString);

            // Configuração temporária para usar banco de dados em memória
            optionsBuilder.UseInMemoryDatabase("ReservaDePassagemDataInMemory");
        }
    }
}
