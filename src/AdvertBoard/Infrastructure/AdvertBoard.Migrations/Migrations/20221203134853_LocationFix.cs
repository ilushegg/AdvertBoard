using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertBoard.Migrations.Migrations
{
    public partial class LocationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "House",
                table: "Location",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "House",
                table: "Location");
        }
    }
}
