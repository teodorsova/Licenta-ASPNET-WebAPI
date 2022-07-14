using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_NET_CORE_API.Migrations
{
    public partial class Addedorderingusertofurnituremodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderingUserId",
                table: "Furnitures",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderingUserId",
                table: "Furnitures");
        }
    }
}
