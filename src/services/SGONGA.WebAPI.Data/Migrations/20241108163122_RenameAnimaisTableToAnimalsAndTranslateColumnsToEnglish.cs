using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGONGA.WebAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameAnimaisTableToAnimalsAndTranslateColumnsToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_animais_tbl_people_TenantId",
                table: "tbl_animais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_animais",
                table: "tbl_animais");

            migrationBuilder.RenameTable(
                name: "tbl_animais",
                newName: "tbl_animals");

            migrationBuilder.RenameColumn(
                name: "Sexo",
                table: "tbl_animals",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "Raca",
                table: "tbl_animals",
                newName: "Breed");

            migrationBuilder.RenameColumn(
                name: "Porte",
                table: "tbl_animals",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "Observacao",
                table: "tbl_animals",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "tbl_animals",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Idade",
                table: "tbl_animals",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "Foto",
                table: "tbl_animals",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "Especie",
                table: "tbl_animals",
                newName: "Species");

            migrationBuilder.RenameColumn(
                name: "Cor",
                table: "tbl_animals",
                newName: "Color");

            migrationBuilder.RenameColumn(
                name: "Castrado",
                table: "tbl_animals",
                newName: "Neutered");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "tbl_animals",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "ChavePix",
                table: "tbl_animals",
                newName: "PixKey");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_animais_TenantId",
                table: "tbl_animals",
                newName: "IX_tbl_animals_TenantId");

            migrationBuilder.AddColumn<bool>(
                name: "Adopted",
                table: "tbl_animals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_animals",
                table: "tbl_animals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_animals_tbl_people_TenantId",
                table: "tbl_animals",
                column: "TenantId",
                principalTable: "tbl_people",
                principalColumn: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_animals_tbl_people_TenantId",
                table: "tbl_animals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_animals",
                table: "tbl_animals");

            migrationBuilder.DropColumn(
                name: "Adopted",
                table: "tbl_animals");

            migrationBuilder.RenameTable(
                name: "tbl_animals",
                newName: "tbl_animais");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "tbl_animais",
                newName: "Sexo");

            migrationBuilder.RenameColumn(
                name: "Breed",
                table: "tbl_animais",
                newName: "Raca");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "tbl_animais",
                newName: "Porte");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "tbl_animais",
                newName: "Observacao");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tbl_animais",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "tbl_animais",
                newName: "Idade");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "tbl_animais",
                newName: "Foto");

            migrationBuilder.RenameColumn(
                name: "Species",
                table: "tbl_animais",
                newName: "Especie");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "tbl_animais",
                newName: "Cor");

            migrationBuilder.RenameColumn(
                name: "Neutered",
                table: "tbl_animais",
                newName: "Castrado");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "tbl_animais",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "PixKey",
                table: "tbl_animais",
                newName: "ChavePix");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_animals_TenantId",
                table: "tbl_animais",
                newName: "IX_tbl_animais_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_animais",
                table: "tbl_animais",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_animais_tbl_people_TenantId",
                table: "tbl_animais",
                column: "TenantId",
                principalTable: "tbl_people",
                principalColumn: "TenantId");
        }
    }
}
