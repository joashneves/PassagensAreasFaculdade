using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace PassagensAreas.Migrations.Voo
{
    /// <inheritdoc />
    public partial class Inicial_VooContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VooSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Origem = table.Column<string>(type: "longtext", nullable: false),
                    Destino = table.Column<string>(type: "longtext", nullable: false),
                    Datas = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Ida = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Volta = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Preco = table.Column<float>(type: "float", nullable: false),
                    Companhias = table.Column<string>(type: "longtext", nullable: false),
                    QuantidadeMaximaPassageiros = table.Column<int>(type: "int", nullable: false),
                    QuantidadePassageiros = table.Column<int>(type: "int", nullable: false),
                    NumeroVoo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VooSet", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VooSet");
        }
    }
}
