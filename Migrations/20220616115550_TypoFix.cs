using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_NET_CORE_API.Migrations
{
    public partial class TypoFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_furnitures_Users_companyNameId",
                table: "furnitures");

            migrationBuilder.DropForeignKey(
                name: "FK_furnituresOrders_furnitures_FurnitureModelId",
                table: "furnituresOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_furnituresOrders_Orders_OrderModelId",
                table: "furnituresOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_furnituresOrders",
                table: "furnituresOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_furnitures",
                table: "furnitures");

            migrationBuilder.RenameTable(
                name: "furnituresOrders",
                newName: "FurnituresOrders");

            migrationBuilder.RenameTable(
                name: "furnitures",
                newName: "Furnitures");

            migrationBuilder.RenameIndex(
                name: "IX_furnituresOrders_OrderModelId",
                table: "FurnituresOrders",
                newName: "IX_FurnituresOrders_OrderModelId");

            migrationBuilder.RenameIndex(
                name: "IX_furnituresOrders_FurnitureModelId",
                table: "FurnituresOrders",
                newName: "IX_FurnituresOrders_FurnitureModelId");

            migrationBuilder.RenameIndex(
                name: "IX_furnitures_companyNameId",
                table: "Furnitures",
                newName: "IX_Furnitures_companyNameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FurnituresOrders",
                table: "FurnituresOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Furnitures",
                table: "Furnitures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Furnitures_Users_companyNameId",
                table: "Furnitures",
                column: "companyNameId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FurnituresOrders_Furnitures_FurnitureModelId",
                table: "FurnituresOrders",
                column: "FurnitureModelId",
                principalTable: "Furnitures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FurnituresOrders_Orders_OrderModelId",
                table: "FurnituresOrders",
                column: "OrderModelId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Furnitures_Users_companyNameId",
                table: "Furnitures");

            migrationBuilder.DropForeignKey(
                name: "FK_FurnituresOrders_Furnitures_FurnitureModelId",
                table: "FurnituresOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_FurnituresOrders_Orders_OrderModelId",
                table: "FurnituresOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FurnituresOrders",
                table: "FurnituresOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Furnitures",
                table: "Furnitures");

            migrationBuilder.RenameTable(
                name: "FurnituresOrders",
                newName: "furnituresOrders");

            migrationBuilder.RenameTable(
                name: "Furnitures",
                newName: "furnitures");

            migrationBuilder.RenameIndex(
                name: "IX_FurnituresOrders_OrderModelId",
                table: "furnituresOrders",
                newName: "IX_furnituresOrders_OrderModelId");

            migrationBuilder.RenameIndex(
                name: "IX_FurnituresOrders_FurnitureModelId",
                table: "furnituresOrders",
                newName: "IX_furnituresOrders_FurnitureModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Furnitures_companyNameId",
                table: "furnitures",
                newName: "IX_furnitures_companyNameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_furnituresOrders",
                table: "furnituresOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_furnitures",
                table: "furnitures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_furnitures_Users_companyNameId",
                table: "furnitures",
                column: "companyNameId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_furnituresOrders_furnitures_FurnitureModelId",
                table: "furnituresOrders",
                column: "FurnitureModelId",
                principalTable: "furnitures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_furnituresOrders_Orders_OrderModelId",
                table: "furnituresOrders",
                column: "OrderModelId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
