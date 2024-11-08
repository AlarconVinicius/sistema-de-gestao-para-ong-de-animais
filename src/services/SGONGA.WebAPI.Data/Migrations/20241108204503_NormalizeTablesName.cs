using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGONGA.WebAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeTablesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_adopters_tbl_people_Id",
                table: "tbl_adopters");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_animals_tbl_people_TenantId",
                table: "tbl_animals");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_organizations_tbl_people_Id",
                table: "tbl_organizations");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_tbl_people_TenantId",
                table: "tbl_people");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_people",
                table: "tbl_people");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_organizations",
                table: "tbl_organizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_animals",
                table: "tbl_animals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_adopters",
                table: "tbl_adopters");

            migrationBuilder.RenameTable(
                name: "tbl_people",
                newName: "People");

            migrationBuilder.RenameTable(
                name: "tbl_organizations",
                newName: "Organizations");

            migrationBuilder.RenameTable(
                name: "tbl_animals",
                newName: "Animals");

            migrationBuilder.RenameTable(
                name: "tbl_adopters",
                newName: "Adopters");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_animals_TenantId",
                table: "Animals",
                newName: "IX_Animals_TenantId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_People_TenantId",
                table: "People",
                column: "TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animals",
                table: "Animals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adopters",
                table: "Adopters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adopters_People_Id",
                table: "Adopters",
                column: "Id",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_People_TenantId",
                table: "Animals",
                column: "TenantId",
                principalTable: "People",
                principalColumn: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_People_Id",
                table: "Organizations",
                column: "Id",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adopters_People_Id",
                table: "Adopters");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_People_TenantId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_People_Id",
                table: "Organizations");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_People_TenantId",
                table: "People");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animals",
                table: "Animals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adopters",
                table: "Adopters");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "tbl_people");

            migrationBuilder.RenameTable(
                name: "Organizations",
                newName: "tbl_organizations");

            migrationBuilder.RenameTable(
                name: "Animals",
                newName: "tbl_animals");

            migrationBuilder.RenameTable(
                name: "Adopters",
                newName: "tbl_adopters");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_TenantId",
                table: "tbl_animals",
                newName: "IX_tbl_animals_TenantId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_tbl_people_TenantId",
                table: "tbl_people",
                column: "TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_people",
                table: "tbl_people",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_organizations",
                table: "tbl_organizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_animals",
                table: "tbl_animals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_adopters",
                table: "tbl_adopters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_adopters_tbl_people_Id",
                table: "tbl_adopters",
                column: "Id",
                principalTable: "tbl_people",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_animals_tbl_people_TenantId",
                table: "tbl_animals",
                column: "TenantId",
                principalTable: "tbl_people",
                principalColumn: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_organizations_tbl_people_Id",
                table: "tbl_organizations",
                column: "Id",
                principalTable: "tbl_people",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
