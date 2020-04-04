using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubeSocial.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ClubeDB");

            migrationBuilder.CreateTable(
                name: "Clubes",
                schema: "ClubeDB",
                columns: table => new
                {
                    ClubeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Decricao = table.Column<string>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubes", x => x.ClubeId);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                schema: "ClubeDB",
                columns: table => new
                {
                    FuncionarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Pelido = table.Column<string>(nullable: true),
                    DataNacimento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.FuncionarioId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "ClubeDB",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "ClubeDB",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Candidatos",
                schema: "ClubeDB",
                columns: table => new
                {
                    ClubeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Pelido = table.Column<string>(nullable: true),
                    DataNacimento = table.Column<DateTime>(nullable: false),
                    Situacao = table.Column<int>(nullable: false),
                    ClubeId1 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatos", x => x.ClubeId);
                    table.ForeignKey(
                        name: "FK_Candidatos_Clubes_ClubeId1",
                        column: x => x.ClubeId1,
                        principalSchema: "ClubeDB",
                        principalTable: "Clubes",
                        principalColumn: "ClubeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cartoes",
                schema: "ClubeDB",
                columns: table => new
                {
                    NumeroDoCartao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    ClubeId = table.Column<int>(nullable: false),
                    DataVencimento = table.Column<DateTime>(nullable: false),
                    Valido = table.Column<bool>(nullable: false),
                    SocioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartoes", x => x.NumeroDoCartao);
                    table.ForeignKey(
                        name: "FK_Cartoes_Clubes_ClubeId",
                        column: x => x.ClubeId,
                        principalSchema: "ClubeDB",
                        principalTable: "Clubes",
                        principalColumn: "ClubeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                schema: "ClubeDB",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "ClubeDB",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                schema: "ClubeDB",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "ClubeDB",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                schema: "ClubeDB",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "ClubeDB",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "ClubeDB",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "ClubeDB",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "ClubeDB",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                schema: "ClubeDB",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "ClubeDB",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Socios",
                schema: "ClubeDB",
                columns: table => new
                {
                    SocioId = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Pelido = table.Column<string>(nullable: true),
                    DataNacimento = table.Column<DateTime>(nullable: false),
                    CartaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socios", x => x.SocioId);
                    table.ForeignKey(
                        name: "FK_Socios_Cartoes_SocioId",
                        column: x => x.SocioId,
                        principalSchema: "ClubeDB",
                        principalTable: "Cartoes",
                        principalColumn: "NumeroDoCartao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dependetes",
                schema: "ClubeDB",
                columns: table => new
                {
                    SocioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Pelido = table.Column<string>(nullable: true),
                    DataNacimento = table.Column<DateTime>(nullable: false),
                    SocioId1 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependetes", x => x.SocioId);
                    table.ForeignKey(
                        name: "FK_Dependetes_Socios_SocioId1",
                        column: x => x.SocioId1,
                        principalSchema: "ClubeDB",
                        principalTable: "Socios",
                        principalColumn: "SocioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mensalidades",
                schema: "ClubeDB",
                columns: table => new
                {
                    MensalidadeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataMensalidade = table.Column<DateTime>(nullable: false),
                    DataVencimento = table.Column<DateTime>(nullable: false),
                    DataPagamento = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Juros = table.Column<decimal>(nullable: false),
                    Pago = table.Column<bool>(nullable: false),
                    SocioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensalidades", x => x.MensalidadeId);
                    table.ForeignKey(
                        name: "FK_Mensalidades_Socios_SocioId",
                        column: x => x.SocioId,
                        principalSchema: "ClubeDB",
                        principalTable: "Socios",
                        principalColumn: "SocioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoriosFuncionarios",
                schema: "ClubeDB",
                columns: table => new
                {
                    MensalidadeId = table.Column<int>(nullable: false),
                    FuncionarioId = table.Column<int>(nullable: false),
                    Descricao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriosFuncionarios", x => new { x.MensalidadeId, x.FuncionarioId });
                    table.ForeignKey(
                        name: "FK_HistoriosFuncionarios_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalSchema: "ClubeDB",
                        principalTable: "Funcionarios",
                        principalColumn: "FuncionarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoriosFuncionarios_Mensalidades_MensalidadeId",
                        column: x => x.MensalidadeId,
                        principalSchema: "ClubeDB",
                        principalTable: "Mensalidades",
                        principalColumn: "MensalidadeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_ClubeId1",
                schema: "ClubeDB",
                table: "Candidatos",
                column: "ClubeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cartoes_ClubeId",
                schema: "ClubeDB",
                table: "Cartoes",
                column: "ClubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependetes_SocioId1",
                schema: "ClubeDB",
                table: "Dependetes",
                column: "SocioId1");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriosFuncionarios_FuncionarioId",
                schema: "ClubeDB",
                table: "HistoriosFuncionarios",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensalidades_SocioId",
                schema: "ClubeDB",
                table: "Mensalidades",
                column: "SocioId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "ClubeDB",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                schema: "ClubeDB",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "ClubeDB",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "ClubeDB",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                schema: "ClubeDB",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                schema: "ClubeDB",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "ClubeDB",
                table: "UserRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidatos",
                schema: "ClubeDB");

            migrationBuilder.DropTable(
                name: "Dependetes",
                schema: "ClubeDB");

            migrationBuilder.DropTable(
                name: "HistoriosFuncionarios",
                schema: "ClubeDB");

            migrationBuilder.DropTable(
                name: "RoleClaim",
                schema: "ClubeDB");

            migrationBuilder.DropTable(
                name: "UserClaim",
                schema: "ClubeDB");

            migrationBuilder.DropTable(
                name: "UserLogin",
                schema: "ClubeDB");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "ClubeDB");

            migrationBuilder.DropTable(
                name: "UserToken",
                schema: "ClubeDB");

            migrationBuilder.DropTable(
                name: "Funcionarios",
                schema: "ClubeDB");

            migrationBuilder.DropTable(
                name: "Mensalidades",
                schema: "ClubeDB");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "ClubeDB");

            migrationBuilder.DropTable(
                name: "User",
                schema: "ClubeDB");

            migrationBuilder.DropTable(
                name: "Socios",
                schema: "ClubeDB");

            migrationBuilder.DropTable(
                name: "Cartoes",
                schema: "ClubeDB");

            migrationBuilder.DropTable(
                name: "Clubes",
                schema: "ClubeDB");
        }
    }
}
