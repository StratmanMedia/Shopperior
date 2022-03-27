using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopperior.Data.EFCore.Migrations
{
    public partial class reset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Item",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        TrashedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Item", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ShoppingList",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        TrashedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ShoppingList", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Store",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        TrashedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Store", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "User",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Idp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IdpSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        TrashedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_User", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoppingListId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrashedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_ShoppingList_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "UserListPermission",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<long>(type: "bigint", nullable: false),
            //        ShoppingListId = table.Column<long>(type: "bigint", nullable: false),
            //        Permission = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserListPermission", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserListPermission_ShoppingList_ShoppingListId",
            //            column: x => x.ShoppingListId,
            //            principalTable: "ShoppingList",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_UserListPermission_User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ListItem",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        ShoppingListId = table.Column<long>(type: "bigint", nullable: false),
            //        ItemId = table.Column<long>(type: "bigint", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        StoreId = table.Column<long>(type: "bigint", nullable: false),
            //        CategoryId = table.Column<long>(type: "bigint", nullable: false),
            //        Quantity = table.Column<double>(type: "float", nullable: false),
            //        Measurement = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        IsInCart = table.Column<bool>(type: "bit", nullable: false),
            //        HasPurchased = table.Column<bool>(type: "bit", nullable: false),
            //        PurchasedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        TrashedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ListItem", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ListItem_Category_CategoryId",
            //            column: x => x.CategoryId,
            //            principalTable: "Category",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ListItem_ShoppingList_ShoppingListId",
            //            column: x => x.ShoppingListId,
            //            principalTable: "ShoppingList",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Category_ShoppingListId",
            //    table: "Category",
            //    column: "ShoppingListId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ListItem_CategoryId",
            //    table: "ListItem",
            //    column: "CategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ListItem_ShoppingListId",
            //    table: "ListItem",
            //    column: "ShoppingListId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserListPermission_ShoppingListId",
            //    table: "UserListPermission",
            //    column: "ShoppingListId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserListPermission_UserId",
            //    table: "UserListPermission",
            //    column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "ListItem");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "UserListPermission");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ShoppingList");
        }
    }
}
