﻿// <auto-generated />
using System;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace PassagensAreas.Migrations.Voo
{
    [DbContext(typeof(VooContext))]
    partial class VooContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PassagensAreas.Domain.Models.VooCliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Companhias")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Datas")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Destino")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Ida")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("NumeroVoo")
                        .HasColumnType("int");

                    b.Property<string>("Origem")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("Preco")
                        .HasColumnType("float");

                    b.Property<int>("QuantidadeMaximaPassageiros")
                        .HasColumnType("int");

                    b.Property<int>("QuantidadePassageiros")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Volta")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("VooSet");
                });
#pragma warning restore 612, 618
        }
    }
}
