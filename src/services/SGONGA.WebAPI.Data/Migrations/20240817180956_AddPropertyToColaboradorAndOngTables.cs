using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGONGA.WebAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyToColaboradorAndOngTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "tbl_ongs");

            migrationBuilder.RenameColumn(
                name: "Endereco_Rua",
                table: "tbl_ongs",
                newName: "Endereco_Logradouro");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "tbl_ongs",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_Estado",
                table: "tbl_ongs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_Complemento",
                table: "tbl_ongs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_Cidade",
                table: "tbl_ongs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_CEP",
                table: "tbl_ongs",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "tbl_ongs",
                type: "varchar(254)",
                maxLength: 254,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(254)",
                oldMaxLength: 254);

            migrationBuilder.AlterColumn<string>(
                name: "ChavePix",
                table: "tbl_ongs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "tbl_ongs",
                type: "varchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Bairro",
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

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "tbl_colaboradores",
                type: "varchar(254)",
                maxLength: 254,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 254);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "tbl_colaboradores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "tbl_colaboradores",
                type: "varchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "tbl_colaboradores",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "tbl_colaboradores",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Documento",
                table: "tbl_ongs");

            migrationBuilder.DropColumn(
                name: "Endereco_Bairro",
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
                name: "DataNascimento",
                table: "tbl_colaboradores");

            migrationBuilder.DropColumn(
                name: "Documento",
                table: "tbl_colaboradores");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "tbl_colaboradores");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "tbl_colaboradores");

            migrationBuilder.RenameColumn(
                name: "Endereco_Logradouro",
                table: "tbl_ongs",
                newName: "Endereco_Rua");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "tbl_ongs",
                type: "varchar(100)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_Estado",
                table: "tbl_ongs",
                type: "varchar(100)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_Complemento",
                table: "tbl_ongs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_Cidade",
                table: "tbl_ongs",
                type: "varchar(100)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_CEP",
                table: "tbl_ongs",
                type: "varchar(100)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "tbl_ongs",
                type: "nvarchar(254)",
                maxLength: 254,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(254)",
                oldMaxLength: 254);

            migrationBuilder.AlterColumn<string>(
                name: "ChavePix",
                table: "tbl_ongs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "tbl_ongs",
                type: "varchar(100)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "tbl_colaboradores",
                type: "varchar(100)",
                maxLength: 254,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(254)",
                oldMaxLength: 254);
        }
    }
}
