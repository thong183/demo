using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.News_Website.Migrations
{
    public partial class addNameUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameUrl",
                table: "AbpUsers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameUrl",
                table: "AbpUsers");
        }
    }
}
