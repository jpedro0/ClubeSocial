using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubeSocial.Migrations
{
    public partial class trocandoChave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoriosFuncionarios",
                schema: "ClubeDB",
                table: "HistoriosFuncionarios");

            migrationBuilder.AddColumn<int>(
                name: "HistorioFuncionarioId",
                schema: "ClubeDB",
                table: "HistoriosFuncionarios",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoriosFuncionarios",
                schema: "ClubeDB",
                table: "HistoriosFuncionarios",
                column: "HistorioFuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriosFuncionarios_MensalidadeId",
                schema: "ClubeDB",
                table: "HistoriosFuncionarios",
                column: "MensalidadeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoriosFuncionarios",
                schema: "ClubeDB",
                table: "HistoriosFuncionarios");

            migrationBuilder.DropIndex(
                name: "IX_HistoriosFuncionarios_MensalidadeId",
                schema: "ClubeDB",
                table: "HistoriosFuncionarios");

            migrationBuilder.DropColumn(
                name: "HistorioFuncionarioId",
                schema: "ClubeDB",
                table: "HistoriosFuncionarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoriosFuncionarios",
                schema: "ClubeDB",
                table: "HistoriosFuncionarios",
                columns: new[] { "MensalidadeId", "FuncionarioId" });
        }
    }
}
