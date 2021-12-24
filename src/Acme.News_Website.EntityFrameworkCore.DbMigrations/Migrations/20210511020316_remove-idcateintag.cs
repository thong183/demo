using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.News_Website.Migrations
{
    public partial class removeidcateintag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTags_AppCategories_IdCategory",
                table: "AppTags");

            migrationBuilder.DropIndex(
                name: "IX_AppTags_IdCategory",
                table: "AppTags");

            migrationBuilder.DropColumn(
                name: "IdCategory",
                table: "AppTags");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdCategory",
                table: "AppTags",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppTags_IdCategory",
                table: "AppTags",
                column: "IdCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTags_AppCategories_IdCategory",
                table: "AppTags",
                column: "IdCategory",
                principalTable: "AppCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
