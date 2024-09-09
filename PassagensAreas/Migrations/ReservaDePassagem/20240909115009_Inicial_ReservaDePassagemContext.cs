using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace PassagensAreas.Migrations.ReservaDePassagem
{
    /// <inheritdoc />
    public partial class Inicial_ReservaDePassagemContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReservaDePassagemSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Id_voo = table.Column<int>(type: "int", nullable: false),
                    CPFCliente = table.Column<string>(type: "longtext", nullable: false),
                    DataReserva = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    NumeroVoo = table.Column<int>(type: "int", nullable: false),
                    AssentosReservados = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaDePassagemSet", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservaDePassagemSet");
        }
    }
}
