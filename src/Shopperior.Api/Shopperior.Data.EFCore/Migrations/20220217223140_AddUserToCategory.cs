using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopperior.Data.EFCore.Migrations
{
    public partial class AddUserToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Category",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ListItem_CategoryId",
                table: "ListItem",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserId",
                table: "Category",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_User_UserId",
                table: "Category",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListItem_Category_CategoryId",
                table: "ListItem",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_UserId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_ListItem_Category_CategoryId",
                table: "ListItem");

            migrationBuilder.DropIndex(
                name: "IX_ListItem_CategoryId",
                table: "ListItem");

            migrationBuilder.DropIndex(
                name: "IX_Category_UserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Category");
        }
    }
}
