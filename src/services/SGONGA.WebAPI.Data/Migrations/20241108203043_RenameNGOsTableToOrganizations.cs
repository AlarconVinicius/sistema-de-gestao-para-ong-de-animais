using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGONGA.WebAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameNGOsTableToOrganizations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ngos");

            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "tbl_people",
                newName: "PersonType");

            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "tbl_people",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "tbl_organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PixKey = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_organizations_tbl_people_Id",
                        column: x => x.Id,
                        principalTable: "tbl_people",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_organizations");

            migrationBuilder.RenameColumn(
                name: "PersonType",
                table: "tbl_people",
                newName: "UserType");

            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "tbl_people",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

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
        }
    }
}
