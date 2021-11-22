using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopperior.Data.EFCore.Migrations
{
    public partial class adduserlistpermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserListPermissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ShoppingListId = table.Column<long>(type: "bigint", nullable: false),
                    Permission = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserListPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserListPermissions_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserListPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserListPermissions_ShoppingListId",
                table: "UserListPermissions",
                column: "ShoppingListId");

            migrationBuilder.CreateIndex(
                name: "IX_UserListPermissions_UserId",
                table: "UserListPermissions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserListPermissions");
        }
    }
}
