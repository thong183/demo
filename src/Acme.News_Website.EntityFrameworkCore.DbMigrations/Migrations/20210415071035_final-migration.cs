using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.News_Website.Migrations
{
    public partial class finalmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppImages_AppBlogs_IdBlog",
                table: "AppImages");

            migrationBuilder.DropIndex(
                name: "IX_AppImages_IdBlog",
                table: "AppImages");

            migrationBuilder.RenameColumn(
                name: "ImgTitle",
                table: "AppImages",
                newName: "UserProfileImg");

            migrationBuilder.RenameColumn(
                name: "IdBlog",
                table: "AppImages",
                newName: "IdUser");

            migrationBuilder.AddColumn<Guid>(
                name: "BlogId",
                table: "AppImages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppBlogImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdBlog = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdImage = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsTitle = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBlogImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppBlogImages_AppBlogs_IdBlog",
                        column: x => x.IdBlog,
                        principalTable: "AppBlogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppBlogImages_AppImages_IdImage",
                        column: x => x.IdImage,
                        principalTable: "AppImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppImages_BlogId",
                table: "AppImages",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBlogImages_IdBlog",
                table: "AppBlogImages",
                column: "IdBlog");

            migrationBuilder.CreateIndex(
                name: "IX_AppBlogImages_IdImage",
                table: "AppBlogImages",
                column: "IdImage");

            migrationBuilder.AddForeignKey(
                name: "FK_AppImages_AppBlogs_BlogId",
                table: "AppImages",
                column: "BlogId",
                principalTable: "AppBlogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppImages_AppBlogs_BlogId",
                table: "AppImages");

            migrationBuilder.DropTable(
                name: "AppBlogImages");

            migrationBuilder.DropIndex(
                name: "IX_AppImages_BlogId",
                table: "AppImages");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "AppImages");

            migrationBuilder.RenameColumn(
                name: "UserProfileImg",
                table: "AppImages",
                newName: "ImgTitle");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "AppImages",
                newName: "IdBlog");

            migrationBuilder.CreateIndex(
                name: "IX_AppImages_IdBlog",
                table: "AppImages",
                column: "IdBlog");

            migrationBuilder.AddForeignKey(
                name: "FK_AppImages_AppBlogs_IdBlog",
                table: "AppImages",
                column: "IdBlog",
                principalTable: "AppBlogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
