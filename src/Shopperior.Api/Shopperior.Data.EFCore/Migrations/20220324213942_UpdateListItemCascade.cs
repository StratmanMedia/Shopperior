using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopperior.Data.EFCore.Migrations
{
    public partial class UpdateListItemCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListItem_Category_CategoryId",
                table: "ListItem");

            migrationBuilder.AddForeignKey(
                name: "FK_ListItem_Category_CategoryId",
                table: "ListItem",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListItem_Category_CategoryId",
                table: "ListItem");

            migrationBuilder.AddForeignKey(
                name: "FK_ListItem_Category_CategoryId",
                table: "ListItem",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
