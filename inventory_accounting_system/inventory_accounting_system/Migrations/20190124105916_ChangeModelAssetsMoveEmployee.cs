using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inventory_accounting_system.Migrations
{
    public partial class ChangeModelAssetsMoveEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCurrent",
                table: "AssetsMoveStories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "DateCurrent",
                table: "AssetsMoveStories");

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
        }
    }
}
