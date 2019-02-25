using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inventory_accounting_system.Migrations
{
    public partial class addAssetsInOrderEmployeeAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssetId",
                table: "OrderEmployeeAdmins",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderEmployeeAdmins_AssetId",
                table: "OrderEmployeeAdmins",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEmployeeAdmins_Assets_AssetId",
                table: "OrderEmployeeAdmins",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderEmployeeAdmins_Assets_AssetId",
                table: "OrderEmployeeAdmins");

            migrationBuilder.DropIndex(
                name: "IX_OrderEmployeeAdmins_AssetId",
                table: "OrderEmployeeAdmins");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "OrderEmployeeAdmins");
        }
    }
}
