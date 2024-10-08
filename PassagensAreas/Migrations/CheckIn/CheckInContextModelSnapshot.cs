﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PassagensAreas.Infraestrutura;

#nullable disable

namespace PassagensAreas.Migrations.CheckIn
{
    [DbContext(typeof(CheckInContext))]
    partial class CheckInContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PassagensAreas.Domain.Models.CheckIn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AssentoEscolhido")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("CheckInRealizado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("DataCheckIn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Id_ReservaDePassagem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CheckInSet");
                });
#pragma warning restore 612, 618
        }
    }
}
