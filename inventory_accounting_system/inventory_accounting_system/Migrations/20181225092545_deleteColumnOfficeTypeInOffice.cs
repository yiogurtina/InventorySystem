using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inventory_accounting_system.Migrations
{
    public partial class deleteColumnOfficeTypeInOffice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offices_OfficeType_OfficeTypeId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Offices_OfficeTypeId",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "OfficeTypeId",
                table: "Offices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfficeTypeId",
                table: "Offices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offices_OfficeTypeId",
                table: "Offices",
                column: "OfficeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_OfficeType_OfficeTypeId",
                table: "Offices",
                column: "OfficeTypeId",
                principalTable: "OfficeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
