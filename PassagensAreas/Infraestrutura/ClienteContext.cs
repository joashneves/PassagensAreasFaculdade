﻿using Microsoft.EntityFrameworkCore;
using PassagensAreas.Domain.Models;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Build.Evaluation;
using Newtonsoft.Json;

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
            var connectionString = _configuration.GetConnectionString("MySQLData");
            optionsBuilder.UseMySQL(connectionString);
        }
        public void InitializeData()
        {
            // Verifica se já existem registros na tabela
            if (ClienteSet.Any())
            {
                // Se houver registros, não faz nada
                return;
            }
            // Caminho para o arquivo JSON
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "ClienteInicial.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Arquivo JSON não encontrado: {filePath}");
            }

            var jsonData = File.ReadAllText(filePath);
            var cliente = JsonConvert.DeserializeObject<List<Cliente>>(jsonData);

            if (cliente != null)
            {
                ClienteSet.AddRange(cliente); 
                SaveChanges(); 
            }
        }
    }
}
