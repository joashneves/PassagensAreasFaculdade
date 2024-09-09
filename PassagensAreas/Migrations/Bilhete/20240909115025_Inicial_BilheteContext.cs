using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace PassagensAreas.Migrations.Bilhete
{
    /// <inheritdoc />
    public partial class Inicial_BilheteContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BilheteSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Id_ReservaDePassagem = table.Column<int>(type: "int", nullable: false),
                    NomeCliente = table.Column<string>(type: "longtext", nullable: false),
                    NumeroVoo = table.Column<string>(type: "longtext", nullable: false),
                    Origem = table.Column<string>(type: "longtext", nullable: false),
                    Destino = table.Column<string>(type: "longtext", nullable: false),
                    DataVoo = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Assento = table.Column<string>(type: "longtext", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BilheteSet", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BilheteSet");
        }
    }
}
