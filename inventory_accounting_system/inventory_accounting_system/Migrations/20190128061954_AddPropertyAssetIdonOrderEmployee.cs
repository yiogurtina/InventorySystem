using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inventory_accounting_system.Migrations
{
    public partial class AddPropertyAssetIdonOrderEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssetId",
                table: "OrderEmployees",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderEmployees_AssetId",
                table: "OrderEmployees",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEmployees_Assets_AssetId",
                table: "OrderEmployees",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderEmployees_Assets_AssetId",
                table: "OrderEmployees");

            migrationBuilder.DropIndex(
                name: "IX_OrderEmployees_AssetId",
                table: "OrderEmployees");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "OrderEmployees");
        }
    }
}
