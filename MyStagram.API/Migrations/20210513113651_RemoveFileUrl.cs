using Microsoft.EntityFrameworkCore.Migrations;

namespace MyStagram.API.Migrations
{
    public partial class RemoveFileUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "Files");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "Files",
                type: "text",
                nullable: true);
        }
    }
}
