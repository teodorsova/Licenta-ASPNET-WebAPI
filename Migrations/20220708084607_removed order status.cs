using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_NET_CORE_API.Migrations
{
    public partial class removedorderstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "AddressLine2");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "AddressLine2",
                table: "Orders",
                newName: "Status");
        }
    }
}
