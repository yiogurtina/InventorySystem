using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inventory_accounting_system.Migrations
{
    public partial class CreateOfficeTypeForStorageAndChangeAsset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetsMoveStories_AspNetUsers_EmployeeId",
                table: "AssetsMoveStories");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetsMoveStories_Offices_OfficeId",
                table: "AssetsMoveStories");

            migrationBuilder.RenameColumn(
                name: "OfficeId",
                table: "AssetsMoveStories",
                newName: "OfficeToId");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "AssetsMoveStories",
                newName: "OfficeFromId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetsMoveStories_OfficeId",
                table: "AssetsMoveStories",
                newName: "IX_AssetsMoveStories_OfficeToId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetsMoveStories_EmployeeId",
                table: "AssetsMoveStories",
                newName: "IX_AssetsMoveStories_OfficeFromId");

            migrationBuilder.AddColumn<string>(
                name: "OfficeTypeId",
                table: "Offices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeFromId",
                table: "AssetsMoveStories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeToId",
                table: "AssetsMoveStories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Assets",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Offices_OfficeTypeId",
                table: "Offices",
                column: "OfficeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetsMoveStories_EmployeeFromId",
                table: "AssetsMoveStories",
                column: "EmployeeFromId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetsMoveStories_EmployeeToId",
                table: "AssetsMoveStories",
                column: "EmployeeToId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_EmployeeId",
                table: "Assets",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AspNetUsers_EmployeeId",
                table: "Assets",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetsMoveStories_AspNetUsers_EmployeeFromId",
                table: "AssetsMoveStories",
                column: "EmployeeFromId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetsMoveStories_AspNetUsers_EmployeeToId",
                table: "AssetsMoveStories",
                column: "EmployeeToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetsMoveStories_Offices_OfficeFromId",
                table: "AssetsMoveStories",
                column: "OfficeFromId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetsMoveStories_Offices_OfficeToId",
                table: "AssetsMoveStories",
                column: "OfficeToId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_OfficeType_OfficeTypeId",
                table: "Offices",
                column: "OfficeTypeId",
                principalTable: "OfficeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AspNetUsers_EmployeeId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetsMoveStories_AspNetUsers_EmployeeFromId",
                table: "AssetsMoveStories");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetsMoveStories_AspNetUsers_EmployeeToId",
                table: "AssetsMoveStories");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetsMoveStories_Offices_OfficeFromId",
                table: "AssetsMoveStories");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetsMoveStories_Offices_OfficeToId",
                table: "AssetsMoveStories");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_OfficeType_OfficeTypeId",
                table: "Offices");

            migrationBuilder.DropTable(
                name: "OfficeType");

            migrationBuilder.DropIndex(
                name: "IX_Offices_OfficeTypeId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_AssetsMoveStories_EmployeeFromId",
                table: "AssetsMoveStories");

            migrationBuilder.DropIndex(
                name: "IX_AssetsMoveStories_EmployeeToId",
                table: "AssetsMoveStories");

            migrationBuilder.DropIndex(
                name: "IX_Assets_EmployeeId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "OfficeTypeId",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "EmployeeFromId",
                table: "AssetsMoveStories");

            migrationBuilder.DropColumn(
                name: "EmployeeToId",
                table: "AssetsMoveStories");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "OfficeToId",
                table: "AssetsMoveStories",
                newName: "OfficeId");

            migrationBuilder.RenameColumn(
                name: "OfficeFromId",
                table: "AssetsMoveStories",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetsMoveStories_OfficeToId",
                table: "AssetsMoveStories",
                newName: "IX_AssetsMoveStories_OfficeId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetsMoveStories_OfficeFromId",
                table: "AssetsMoveStories",
                newName: "IX_AssetsMoveStories_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetsMoveStories_AspNetUsers_EmployeeId",
                table: "AssetsMoveStories",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetsMoveStories_Offices_OfficeId",
                table: "AssetsMoveStories",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
