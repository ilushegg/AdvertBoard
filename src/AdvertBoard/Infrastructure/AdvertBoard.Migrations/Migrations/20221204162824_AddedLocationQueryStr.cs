using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertBoard.Migrations.Migrations
{
    public partial class AddedLocationQueryStr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Location_LocationId",
                table: "Advertisements");

            migrationBuilder.AddColumn<string>(
                name: "LocationQueryString",
                table: "Location",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Advertisements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Location_LocationId",
                table: "Advertisements",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Location_LocationId",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "LocationQueryString",
                table: "Location");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Advertisements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Location_LocationId",
                table: "Advertisements",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");
        }
    }
}
