using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_NET_CORE_API.Migrations
{
    public partial class addedorderdateandfurniturestatus2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "date",
                table: "Orders",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Orders",
                newName: "date");
        }
    }
}
