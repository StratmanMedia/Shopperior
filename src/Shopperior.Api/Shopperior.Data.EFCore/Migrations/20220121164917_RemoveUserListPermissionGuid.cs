using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopperior.Data.EFCore.Migrations
{
    public partial class RemoveUserListPermissionGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "UserListPermission");

            migrationBuilder.DropColumn(
                name: "LastModifiedTime",
                table: "UserListPermission");

            migrationBuilder.DropColumn(
                name: "TrashedTime",
                table: "UserListPermission");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "UserListPermission",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedTime",
                table: "UserListPermission",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TrashedTime",
                table: "UserListPermission",
                type: "datetime2",
                nullable: true);
        }
    }
}
