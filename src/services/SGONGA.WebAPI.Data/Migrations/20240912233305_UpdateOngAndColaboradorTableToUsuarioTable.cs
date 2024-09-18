using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGONGA.WebAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOngAndColaboradorTableToUsuarioTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_colaboradores");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "tbl_ongs");

            migrationBuilder.DropColumn(
                name: "Documento",
                table: "tbl_ongs");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "tbl_ongs");

            migrationBuilder.DropColumn(
                name: "Endereco_Bairro",
                table: "tbl_ongs");

            migrationBuilder.DropColumn(
                name: "Endereco_CEP",
                table: "tbl_ongs");

            migrationBuilder.DropColumn(
                name: "Endereco_Cidade",
                table: "tbl_ongs");

            migrationBuilder.DropColumn(
                name: "Endereco_Complemento",
                table: "tbl_ongs");

            migrationBuilder.DropColumn(
                name: "Endereco_Estado",
                table: "tbl_ongs");

            migrationBuilder.DropColumn(
                name: "Endereco_Logradouro",
                table: "tbl_ongs");

            migrationBuilder.DropColumn(
                name: "Endereco_Numero",
                table: "tbl_ongs");

            migrationBuilder.DropColumn(
                name: "Endereco_Referencia",
                table: "tbl_ongs");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "tbl_ongs");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "tbl_ongs");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "tbl_ongs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "tbl_ongs");

            migrationBuilder.CreateTable(
                name: "tbl_usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioTipo = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Apelido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Documento = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    Site = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Telefone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false),
                    TelefoneVisivel = table.Column<bool>(type: "bit", nullable: false),
                    AssinarNewsletter = table.Column<bool>(type: "bit", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Cidade = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Sobre = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_adotantes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_adotantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_adotantes_tbl_usuarios_Id",
                        column: x => x.Id,
                        principalTable: "tbl_usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ongs_tbl_usuarios_Id",
                table: "tbl_ongs",
                column: "Id",
                principalTable: "tbl_usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ongs_tbl_usuarios_Id",
                table: "tbl_ongs");

            migrationBuilder.DropTable(
                name: "tbl_adotantes");

            migrationBuilder.DropTable(
                name: "tbl_usuarios");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "tbl_ongs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "tbl_ongs",
                type: "varchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "tbl_ongs",
                type: "varchar(254)",
                maxLength: 254,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Bairro",
                table: "tbl_ongs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_CEP",
                table: "tbl_ongs",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Cidade",
                table: "tbl_ongs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Complemento",
                table: "tbl_ongs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Estado",
                table: "tbl_ongs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Logradouro",
                table: "tbl_ongs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Endereco_Numero",
                table: "tbl_ongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Referencia",
                table: "tbl_ongs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "tbl_ongs",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "tbl_ongs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "tbl_ongs",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "tbl_ongs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "tbl_colaboradores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Documento = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false),
                    Telefone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_colaboradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_colaboradores_tbl_ongs_TenantId",
                        column: x => x.TenantId,
                        principalTable: "tbl_ongs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_colaboradores_TenantId",
                table: "tbl_colaboradores",
                column: "TenantId");
        }
    }
}
