using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertBoard.Migrations.Migrations
{
    public partial class FixUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
