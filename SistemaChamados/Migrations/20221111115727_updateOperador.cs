using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaChamados.Migrations
{
    public partial class updateOperador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Operador",
                table: "Calls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Operador",
                table: "Calls");
        }
    }
}
