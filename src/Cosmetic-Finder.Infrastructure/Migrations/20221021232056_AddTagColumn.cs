using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cosmetic_Finder.Infrastructure.Migrations
{
    public partial class AddTagColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Compose",
                table: "Tags",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Compose",
                table: "Tags");
        }
    }
}
