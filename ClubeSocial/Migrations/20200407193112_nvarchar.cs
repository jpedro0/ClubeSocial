using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubeSocial.Migrations
{
    public partial class nvarchar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                schema: "ClubeDB",
                table: "HistoriosFuncionarios",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Descricao",
                schema: "ClubeDB",
                table: "HistoriosFuncionarios",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");
        }
    }
}
