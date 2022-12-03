using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertBoard.Migrations.Migrations
{
    public partial class AddedLatLonPropertiesToLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Location",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lon",
                table: "Location",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isActived",
                table: "Advertisements",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "Lon",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "isActived",
                table: "Advertisements");
        }
    }
}
