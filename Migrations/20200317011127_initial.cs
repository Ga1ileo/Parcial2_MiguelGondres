using Microsoft.EntityFrameworkCore.Migrations;

namespace Parcial2_MiguelGondres.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Llamada",
                columns: table => new
                {
                    LlamadaId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Llamada", x => x.LlamadaId);
                });

            migrationBuilder.CreateTable(
                name: "llamadaDetalles",
                columns: table => new
                {
                    LlamadaDetalleId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LlamadaId = table.Column<int>(nullable: false),
                    Problemas = table.Column<string>(nullable: true),
                    Solucion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_llamadaDetalles", x => x.LlamadaDetalleId);
                    table.ForeignKey(
                        name: "FK_llamadaDetalles_Llamada_LlamadaId",
                        column: x => x.LlamadaId,
                        principalTable: "Llamada",
                        principalColumn: "LlamadaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_llamadaDetalles_LlamadaId",
                table: "llamadaDetalles",
                column: "LlamadaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "llamadaDetalles");

            migrationBuilder.DropTable(
                name: "Llamada");
        }
    }
}
