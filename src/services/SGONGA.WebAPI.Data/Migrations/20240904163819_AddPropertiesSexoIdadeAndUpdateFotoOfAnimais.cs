using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGONGA.WebAPI.Data.Migrations;

/// <inheritdoc />
public partial class AddPropertiesSexoIdadeAndUpdateFotoOfAnimais : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Fotos",
            table: "tbl_animais");

        migrationBuilder.AlterColumn<string>(
            name: "Raca",
            table: "tbl_animais",
            type: "varchar(50)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(100)",
            oldMaxLength: 50);

        migrationBuilder.AlterColumn<string>(
            name: "Porte",
            table: "tbl_animais",
            type: "varchar(50)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(100)",
            oldMaxLength: 50);

        migrationBuilder.AlterColumn<string>(
            name: "Observacao",
            table: "tbl_animais",
            type: "varchar(500)",
            maxLength: 500,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(100)",
            oldMaxLength: 500);

        migrationBuilder.AlterColumn<string>(
            name: "Especie",
            table: "tbl_animais",
            type: "varchar(50)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(100)",
            oldMaxLength: 50);

        migrationBuilder.AlterColumn<string>(
            name: "Descricao",
            table: "tbl_animais",
            type: "varchar(500)",
            maxLength: 500,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(100)",
            oldMaxLength: 500);

        migrationBuilder.AlterColumn<string>(
            name: "Cor",
            table: "tbl_animais",
            type: "varchar(50)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(100)",
            oldMaxLength: 50);

        migrationBuilder.AddColumn<bool>(
            name: "Castrado",
            table: "tbl_animais",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<string>(
            name: "Foto",
            table: "tbl_animais",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "Idade",
            table: "tbl_animais",
            type: "varchar(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<bool>(
            name: "Sexo",
            table: "tbl_animais",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Castrado",
            table: "tbl_animais");

        migrationBuilder.DropColumn(
            name: "Foto",
            table: "tbl_animais");

        migrationBuilder.DropColumn(
            name: "Idade",
            table: "tbl_animais");

        migrationBuilder.DropColumn(
            name: "Sexo",
            table: "tbl_animais");

        migrationBuilder.AlterColumn<string>(
            name: "Raca",
            table: "tbl_animais",
            type: "varchar(100)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(50)",
            oldMaxLength: 50);

        migrationBuilder.AlterColumn<string>(
            name: "Porte",
            table: "tbl_animais",
            type: "varchar(100)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(50)",
            oldMaxLength: 50);

        migrationBuilder.AlterColumn<string>(
            name: "Observacao",
            table: "tbl_animais",
            type: "varchar(100)",
            maxLength: 500,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(500)",
            oldMaxLength: 500);

        migrationBuilder.AlterColumn<string>(
            name: "Especie",
            table: "tbl_animais",
            type: "varchar(100)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(50)",
            oldMaxLength: 50);

        migrationBuilder.AlterColumn<string>(
            name: "Descricao",
            table: "tbl_animais",
            type: "varchar(100)",
            maxLength: 500,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(500)",
            oldMaxLength: 500);

        migrationBuilder.AlterColumn<string>(
            name: "Cor",
            table: "tbl_animais",
            type: "varchar(100)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(50)",
            oldMaxLength: 50);

        migrationBuilder.AddColumn<string>(
            name: "Fotos",
            table: "tbl_animais",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }
}
