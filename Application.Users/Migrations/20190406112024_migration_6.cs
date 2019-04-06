using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Users.Migrations
{
    public partial class migration_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "user");

            migrationBuilder.DropColumn(
                name: "AccessTokenExpiry",
                table: "user");

            migrationBuilder.DropColumn(
                name: "AccessTokenLifetime",
                table: "user");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "user");

            migrationBuilder.DropColumn(
                name: "RefreshTokenLifetime",
                table: "user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "user",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AccessTokenExpiry",
                table: "user",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "AccessTokenLifetime",
                table: "user",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "user",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "RefreshTokenLifetime",
                table: "user",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
