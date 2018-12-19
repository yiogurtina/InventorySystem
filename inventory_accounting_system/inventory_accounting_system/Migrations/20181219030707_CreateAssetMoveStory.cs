using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inventory_accounting_system.Migrations
{
    public partial class CreateAssetMoveStory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AspNetUsers_EmployeeId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Storages_StorageId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Storages_StorageId",
                table: "Suppliers");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_StorageId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Assets_EmployeeId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_StorageId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Assets");

            migrationBuilder.CreateTable(
                name: "AssetsMoveStories",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AssetId = table.Column<string>(nullable: true),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<string>(nullable: true),
                    OfficeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetsMoveStories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetsMoveStories_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetsMoveStories_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetsMoveStories_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetsMoveStories_AssetId",
                table: "AssetsMoveStories",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetsMoveStories_EmployeeId",
                table: "AssetsMoveStories",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetsMoveStories_OfficeId",
                table: "AssetsMoveStories",
                column: "OfficeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetsMoveStories");

            migrationBuilder.AddColumn<string>(
                name: "StorageId",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Assets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageId",
                table: "Assets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    EmployeeId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Storages_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_StorageId",
                table: "Suppliers",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_EmployeeId",
                table: "Assets",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_StorageId",
                table: "Assets",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_EmployeeId",
                table: "Storages",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AspNetUsers_EmployeeId",
                table: "Assets",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Storages_StorageId",
                table: "Assets",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Storages_StorageId",
                table: "Suppliers",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
