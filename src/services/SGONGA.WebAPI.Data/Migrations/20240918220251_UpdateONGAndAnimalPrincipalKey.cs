using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGONGA.WebAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateONGAndAnimalPrincipalKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_animais_tbl_ongs_TenantId",
                table: "tbl_animais");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_tbl_usuarios_TenantId",
                table: "tbl_usuarios",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_animais_tbl_usuarios_TenantId",
                table: "tbl_animais",
                column: "TenantId",
                principalTable: "tbl_usuarios",
                principalColumn: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_animais_tbl_usuarios_TenantId",
                table: "tbl_animais");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_tbl_usuarios_TenantId",
                table: "tbl_usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_animais_tbl_ongs_TenantId",
                table: "tbl_animais",
                column: "TenantId",
                principalTable: "tbl_ongs",
                principalColumn: "Id");
        }
    }
}
