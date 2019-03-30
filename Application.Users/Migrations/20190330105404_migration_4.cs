using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Users.Migrations
{
    public partial class migration_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "status");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "status",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "role",
                nullable: true);
        }
    }
}
