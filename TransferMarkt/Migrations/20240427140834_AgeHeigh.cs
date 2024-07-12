using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransferMarkt.Migrations
{
    public partial class AgeHeigh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Playerss",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Playerss",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Playerss");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Playerss");
        }
    }
}
