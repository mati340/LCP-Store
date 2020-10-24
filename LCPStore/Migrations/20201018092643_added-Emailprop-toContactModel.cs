using Microsoft.EntityFrameworkCore.Migrations;

namespace LCPStore.Migrations
{
    public partial class addedEmailproptoContactModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Contact",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Contact");
        }
    }
}
