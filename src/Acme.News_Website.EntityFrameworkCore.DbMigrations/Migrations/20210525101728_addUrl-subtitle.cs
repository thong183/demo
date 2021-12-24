using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.News_Website.Migrations
{
    public partial class addUrlsubtitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TagUrl",
                table: "AppTags",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryUrl",
                table: "AppCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "AppBlogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagUrl",
                table: "AppTags");

            migrationBuilder.DropColumn(
                name: "CategoryUrl",
                table: "AppCategories");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "AppBlogs");
        }
    }
}
