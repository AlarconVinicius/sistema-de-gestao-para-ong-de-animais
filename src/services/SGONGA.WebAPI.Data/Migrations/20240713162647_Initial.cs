using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGONGA.WebAPI.Data.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "tbl_ongs",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                Descricao = table.Column<string>(type: "varchar(100)", maxLength: 500, nullable: false),
                Telefone = table.Column<string>(type: "varchar(100)", maxLength: 15, nullable: false),
                Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                Endereco_Rua = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                Endereco_Cidade = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false),
                Endereco_Estado = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false),
                Endereco_CEP = table.Column<string>(type: "varchar(100)", maxLength: 20, nullable: false),
                Endereco_Complemento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                ChavePix = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_tbl_ongs", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "tbl_animais",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                Especie = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false),
                Raca = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false),
                Cor = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false),
                Porte = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false),
                Descricao = table.Column<string>(type: "varchar(100)", maxLength: 500, nullable: false),
                Observacao = table.Column<string>(type: "varchar(100)", maxLength: 500, nullable: false),
                ChavePix = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                Fotos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_tbl_animais", x => x.Id);
                table.ForeignKey(
                    name: "FK_tbl_animais_tbl_ongs_TenantId",
                    column: x => x.TenantId,
                    principalTable: "tbl_ongs",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "tbl_colaboradores",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Email = table.Column<string>(type: "varchar(100)", maxLength: 254, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
            name: "IX_tbl_animais_TenantId",
            table: "tbl_animais",
            column: "TenantId");

        migrationBuilder.CreateIndex(
            name: "IX_tbl_colaboradores_TenantId",
            table: "tbl_colaboradores",
            column: "TenantId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "tbl_animais");

        migrationBuilder.DropTable(
            name: "tbl_colaboradores");

        migrationBuilder.DropTable(
            name: "tbl_ongs");
    }
}
