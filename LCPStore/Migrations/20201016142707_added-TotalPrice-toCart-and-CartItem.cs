using Microsoft.EntityFrameworkCore.Migrations;

namespace LCPStore.Migrations
{
    public partial class addedTotalPricetoCartandCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "SumToPay",
                table: "Cart");
        }
    }
}
