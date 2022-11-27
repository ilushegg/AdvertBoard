using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertBoard.Migrations.Migrations
{
    public partial class ChangedImagesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Products_ProductId",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAvatar_Users_UserId",
                table: "UserAvatar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAvatar",
                table: "UserAvatar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImage",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "UserAvatar");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "ProductImage");

            migrationBuilder.RenameTable(
                name: "UserAvatar",
                newName: "UserAvatars");

            migrationBuilder.RenameTable(
                name: "ProductImage",
                newName: "ProductImages");

            migrationBuilder.RenameIndex(
                name: "IX_UserAvatar_UserId",
                table: "UserAvatars",
                newName: "IX_UserAvatars_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductId");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "UserAvatars",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "ProductImages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAvatars",
                table: "UserAvatars",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAvatars_ImageId",
                table: "UserAvatars",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ImageId",
                table: "ProductImages",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Images_ImageId",
                table: "ProductImages",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAvatars_Images_ImageId",
                table: "UserAvatars",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAvatars_Users_UserId",
                table: "UserAvatars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Images_ImageId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAvatars_Images_ImageId",
                table: "UserAvatars");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAvatars_Users_UserId",
                table: "UserAvatars");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAvatars",
                table: "UserAvatars");

            migrationBuilder.DropIndex(
                name: "IX_UserAvatars_ImageId",
                table: "UserAvatars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ImageId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "UserAvatars");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "ProductImages");

            migrationBuilder.RenameTable(
                name: "UserAvatars",
                newName: "UserAvatar");

            migrationBuilder.RenameTable(
                name: "ProductImages",
                newName: "ProductImage");

            migrationBuilder.RenameIndex(
                name: "IX_UserAvatars_UserId",
                table: "UserAvatar",
                newName: "IX_UserAvatar_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImage",
                newName: "IX_ProductImage_ProductId");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "UserAvatar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "ProductImage",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAvatar",
                table: "UserAvatar",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImage",
                table: "ProductImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Products_ProductId",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAvatar_Users_UserId",
                table: "UserAvatar",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
