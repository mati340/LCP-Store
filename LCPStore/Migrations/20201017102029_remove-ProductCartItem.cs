using Microsoft.EntityFrameworkCore.Migrations;

namespace LCPStore.Migrations
{
    public partial class removeProductCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCartItem");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CartItem",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "CartItem",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SumToPay",
                table: "Cart",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Product_ProductId",
                table: "CartItem",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Product_ProductId",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "SumToPay",
                table: "Cart");

            migrationBuilder.CreateTable(
                name: "ProductCartItem",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCartItem", x => new { x.ProductId, x.CartItemId });
                    table.ForeignKey(
                        name: "FK_ProductCartItem_CartItem_CartItemId",
                        column: x => x.CartItemId,
                        principalTable: "CartItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCartItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCartItem_CartItemId",
                table: "ProductCartItem",
                column: "CartItemId");
        }
    }
}
