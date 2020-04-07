using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubeSocial.Migrations
{
    public partial class DataCadastroSocio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                schema: "ClubeDB",
                table: "Socios",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCadastro",
                schema: "ClubeDB",
                table: "Socios");
        }
    }
}
