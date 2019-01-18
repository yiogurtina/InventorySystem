using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inventory_accounting_system.Migrations
{
    public partial class OfficeStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Storages_StorageId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_StorageId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Assets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StorageId",
                table: "Assets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_StorageId",
                table: "Assets",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Storages_StorageId",
                table: "Assets",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
