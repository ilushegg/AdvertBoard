using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertBoard.Migrations.Migrations
{
    public partial class FixFavoriteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Favorites_AdvertisementId",
                table: "Favorites",
                column: "AdvertisementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Advertisements_AdvertisementId",
                table: "Favorites",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Advertisements_AdvertisementId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_AdvertisementId",
                table: "Favorites");
        }
    }
}
