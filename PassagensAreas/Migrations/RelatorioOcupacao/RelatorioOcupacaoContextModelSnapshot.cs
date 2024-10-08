﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PassagensAreas.Infraestrutura;

#nullable disable

namespace PassagensAreas.Migrations.RelatorioOcupacao
{
    [DbContext(typeof(RelatorioOcupacaoContext))]
    partial class RelatorioOcupacaoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PassagensAreas.Domain.Models.RelatorioOcupacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataRelatorio")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("PercentualOcupacao")
                        .HasColumnType("double");

                    b.Property<int>("VooId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RelatorioOcupacaoSet");
                });
#pragma warning restore 612, 618
        }
    }
}
