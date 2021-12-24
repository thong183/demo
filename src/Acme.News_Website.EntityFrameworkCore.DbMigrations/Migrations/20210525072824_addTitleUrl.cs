using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.News_Website.Migrations
{
    public partial class addTitleUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TitleUrl",
                table: "AppBlogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleUrl",
                table: "AppBlogs");
        }
    }
}
