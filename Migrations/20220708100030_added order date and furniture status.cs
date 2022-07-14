using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_NET_CORE_API.Migrations
{
    public partial class addedorderdateandfurniturestatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Furnitures",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Furnitures");
        }
    }
}
