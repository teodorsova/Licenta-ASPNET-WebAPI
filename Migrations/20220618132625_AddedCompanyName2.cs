using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_NET_CORE_API.Migrations
{
    public partial class AddedCompanyName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Furnitures",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Furnitures");
        }
    }
}
