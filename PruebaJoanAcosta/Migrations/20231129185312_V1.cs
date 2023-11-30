using Microsoft.EntityFrameworkCore.Migrations;

namespace PruebaJoanAcosta.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deportistas",
                columns: table => new
                {
                    IdDeportista = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaisDeportista = table.Column<string>(nullable: true),
                    NombreDeportista = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deportistas", x => x.IdDeportista);
                });

            migrationBuilder.CreateTable(
                name: "DeportistaPeso",
                columns: table => new
                {
                    IdDeporPeso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDeportistaFk = table.Column<int>(nullable: false),
                    Arranque = table.Column<int>(nullable: false),
                    Envion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeportistaPeso", x => x.IdDeporPeso);
                    table.ForeignKey(
                        name: "FK_DeportistaPeso_Deportistas_IdDeportistaFk",
                        column: x => x.IdDeportistaFk,
                        principalTable: "Deportistas",
                        principalColumn: "IdDeportista",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeportistaPeso_IdDeportistaFk",
                table: "DeportistaPeso",
                column: "IdDeportistaFk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeportistaPeso");

            migrationBuilder.DropTable(
                name: "Deportistas");
        }
    }
}
