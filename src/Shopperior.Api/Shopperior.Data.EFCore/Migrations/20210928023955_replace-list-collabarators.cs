using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopperior.Data.EFCore.Migrations
{
    public partial class replacelistcollabarators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingListUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingListUser",
                columns: table => new
                {
                    CollaboratorsId = table.Column<long>(type: "bigint", nullable: false),
                    ShoppingListsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListUser", x => new { x.CollaboratorsId, x.ShoppingListsId });
                    table.ForeignKey(
                        name: "FK_ShoppingListUser_ShoppingLists_ShoppingListsId",
                        column: x => x.ShoppingListsId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingListUser_Users_CollaboratorsId",
                        column: x => x.CollaboratorsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListUser_ShoppingListsId",
                table: "ShoppingListUser",
                column: "ShoppingListsId");
        }
    }
}
