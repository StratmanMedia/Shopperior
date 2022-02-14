using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopperior.Data.EFCore.Migrations
{
    public partial class AddedShoppingListRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnteredCartTime",
                table: "ListItem");

            migrationBuilder.CreateIndex(
                name: "IX_UserListPermission_ShoppingListId",
                table: "UserListPermission",
                column: "ShoppingListId");

            migrationBuilder.CreateIndex(
                name: "IX_UserListPermission_UserId",
                table: "UserListPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ListItem_ShoppingListId",
                table: "ListItem",
                column: "ShoppingListId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListItem_ShoppingList_ShoppingListId",
                table: "ListItem",
                column: "ShoppingListId",
                principalTable: "ShoppingList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserListPermission_ShoppingList_ShoppingListId",
                table: "UserListPermission",
                column: "ShoppingListId",
                principalTable: "ShoppingList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserListPermission_User_UserId",
                table: "UserListPermission",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListItem_ShoppingList_ShoppingListId",
                table: "ListItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UserListPermission_ShoppingList_ShoppingListId",
                table: "UserListPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_UserListPermission_User_UserId",
                table: "UserListPermission");

            migrationBuilder.DropIndex(
                name: "IX_UserListPermission_ShoppingListId",
                table: "UserListPermission");

            migrationBuilder.DropIndex(
                name: "IX_UserListPermission_UserId",
                table: "UserListPermission");

            migrationBuilder.DropIndex(
                name: "IX_ListItem_ShoppingListId",
                table: "ListItem");

            migrationBuilder.AddColumn<DateTime>(
                name: "EnteredCartTime",
                table: "ListItem",
                type: "datetime2",
                nullable: true);
        }
    }
}
