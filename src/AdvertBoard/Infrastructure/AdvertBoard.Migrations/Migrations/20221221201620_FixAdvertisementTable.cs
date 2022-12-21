using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertBoard.Migrations.Migrations
{
    public partial class FixAdvertisementTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActived",
                table: "Advertisements");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Advertisements",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Advertisements");

            migrationBuilder.AddColumn<bool>(
                name: "isActived",
                table: "Advertisements",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
