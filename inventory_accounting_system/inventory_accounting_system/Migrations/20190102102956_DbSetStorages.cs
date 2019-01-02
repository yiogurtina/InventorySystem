using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inventory_accounting_system.Migrations
{
    public partial class DbSetStorages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Storage_StorageId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Storage_AspNetUsers_OwnerId",
                table: "Storage");

            migrationBuilder.DropTable(
                name: "OfficeType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Storage",
                table: "Storage");

            migrationBuilder.RenameTable(
                name: "Storage",
                newName: "Storages");

            migrationBuilder.RenameIndex(
                name: "IX_Storage_OwnerId",
                table: "Storages",
                newName: "IX_Storages_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Storages",
                table: "Storages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Storages_StorageId",
                table: "Assets",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_AspNetUsers_OwnerId",
                table: "Storages",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Storages_StorageId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_AspNetUsers_OwnerId",
                table: "Storages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Storages",
                table: "Storages");

            migrationBuilder.RenameTable(
                name: "Storages",
                newName: "Storage");

            migrationBuilder.RenameIndex(
                name: "IX_Storages_OwnerId",
                table: "Storage",
                newName: "IX_Storage_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Storage",
                table: "Storage",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OfficeType",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeType", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Storage_StorageId",
                table: "Assets",
                column: "StorageId",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_AspNetUsers_OwnerId",
                table: "Storage",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
