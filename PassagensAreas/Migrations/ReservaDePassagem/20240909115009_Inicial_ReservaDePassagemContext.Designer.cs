﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PassagensAreas.Infraestrutura;

#nullable disable

namespace PassagensAreas.Migrations.ReservaDePassagem
{
    [DbContext(typeof(ReservaDePassagemContext))]
    [Migration("20240909115009_Inicial_ReservaDePassagemContext")]
    partial class Inicial_ReservaDePassagemContext
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PassagensAreas.Domain.Models.ReservaDePassagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AssentosReservados")
                        .HasColumnType("int");

                    b.Property<string>("CPFCliente")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DataReserva")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Id_voo")
                        .HasColumnType("int");

                    b.Property<int>("NumeroVoo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ReservaDePassagemSet");
                });
#pragma warning restore 612, 618
        }
    }
}
