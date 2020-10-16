using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCPStore.Migrations
{
    public partial class addedContactModelCategorypage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Product");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Product");

            migrationBuilder.AddColumn<double>(
                name: "InStock",
                table: "Product",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
