using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGONGA.WebAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSolicitacaoCadastroTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_solicitacoes_cadastro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_solicitacoes_cadastro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChavePixOng = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataNascimentoResponsavel = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentoOng = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    DocumentoResponsavel = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    IdOng = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdResponsavel = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstagramOng = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    NomeOng = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    NomeResponsavel = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailOng = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false),
                    TelefoneOng = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    EmailResponsavel = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false),
                    TelefoneResponsavel = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    EnderecoOng_Bairro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    EnderecoOng_CEP = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    EnderecoOng_Cidade = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    EnderecoOng_Complemento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    EnderecoOng_Estado = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    EnderecoOng_Logradouro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    EnderecoOng_Numero = table.Column<int>(type: "int", nullable: false),
                    EnderecoOng_Referencia = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_solicitacoes_cadastro", x => x.Id);
                });
        }
    }
}
