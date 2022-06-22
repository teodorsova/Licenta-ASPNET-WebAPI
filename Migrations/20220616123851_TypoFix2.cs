using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_NET_CORE_API.Migrations
{
    public partial class TypoFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Furnitures_Users_companyNameId",
                table: "Furnitures");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "Furnitures",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "companyNameId",
                table: "Furnitures",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Furnitures_companyNameId",
                table: "Furnitures",
                newName: "IX_Furnitures_CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Furnitures_Users_CompanyId",
                table: "Furnitures",
                column: "CompanyId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Furnitures_Users_CompanyId",
                table: "Furnitures");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Furnitures",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Furnitures",
                newName: "companyNameId");

            migrationBuilder.RenameIndex(
                name: "IX_Furnitures_CompanyId",
                table: "Furnitures",
                newName: "IX_Furnitures_companyNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Furnitures_Users_companyNameId",
                table: "Furnitures",
                column: "companyNameId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
