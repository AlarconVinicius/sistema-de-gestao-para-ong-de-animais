using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGONGA.WebAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameUsuariosTableToPeopleAndTranslateColumnsToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_animais_tbl_usuarios_TenantId",
                table: "tbl_animais");

            migrationBuilder.DropTable(
                name: "tbl_adotantes");

            migrationBuilder.DropTable(
                name: "tbl_ongs");

            migrationBuilder.DropTable(
                name: "tbl_usuarios");

            migrationBuilder.CreateTable(
                name: "tbl_people",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Nickname = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Document = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    Site = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    IsPhoneNumberVisible = table.Column<bool>(type: "bit", nullable: false),
                    SubscribeToNewsletter = table.Column<bool>(type: "bit", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    About = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_people", x => x.Id);
                    table.UniqueConstraint("AK_tbl_people_TenantId", x => x.TenantId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_adopters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_adopters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_adopters_tbl_people_Id",
                        column: x => x.Id,
                        principalTable: "tbl_people",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ngos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PixKey = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ngos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_ngos_tbl_people_Id",
                        column: x => x.Id,
                        principalTable: "tbl_people",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_animais_tbl_people_TenantId",
                table: "tbl_animais",
                column: "TenantId",
                principalTable: "tbl_people",
                principalColumn: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_animais_tbl_people_TenantId",
                table: "tbl_animais");

            migrationBuilder.DropTable(
                name: "tbl_adopters");

            migrationBuilder.DropTable(
                name: "tbl_ngos");

            migrationBuilder.DropTable(
                name: "tbl_people");

            migrationBuilder.CreateTable(
                name: "tbl_usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Apelido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    AssinarNewsletter = table.Column<bool>(type: "bit", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Documento = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    Estado = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Site = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Slug = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Sobre = table.Column<string>(type: "text", nullable: true),
                    TelefoneVisivel = table.Column<bool>(type: "bit", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioTipo = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false),
                    Telefone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_usuarios", x => x.Id);
                    table.UniqueConstraint("AK_tbl_usuarios_TenantId", x => x.TenantId);
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

            migrationBuilder.CreateTable(
                name: "tbl_ongs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChavePix = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_ongs_tbl_usuarios_Id",
                        column: x => x.Id,
                        principalTable: "tbl_usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_animais_tbl_usuarios_TenantId",
                table: "tbl_animais",
                column: "TenantId",
                principalTable: "tbl_usuarios",
                principalColumn: "TenantId");
        }
    }
}
