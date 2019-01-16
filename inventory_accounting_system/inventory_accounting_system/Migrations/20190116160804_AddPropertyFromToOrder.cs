using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inventory_accounting_system.Migrations
{
    public partial class AddPropertyFromToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderEmployees_AspNetUsers_EmployeeId",
                table: "OrderEmployees");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "OrderEmployees",
                newName: "EmployeeToId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderEmployees_EmployeeId",
                table: "OrderEmployees",
                newName: "IX_OrderEmployees_EmployeeToId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTo",
                table: "OrderEmployees",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmployeeFromId",
                table: "OrderEmployees",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderEmployees_EmployeeFromId",
                table: "OrderEmployees",
                column: "EmployeeFromId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEmployees_AspNetUsers_EmployeeFromId",
                table: "OrderEmployees",
                column: "EmployeeFromId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEmployees_AspNetUsers_EmployeeToId",
                table: "OrderEmployees",
                column: "EmployeeToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderEmployees_AspNetUsers_EmployeeFromId",
                table: "OrderEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEmployees_AspNetUsers_EmployeeToId",
                table: "OrderEmployees");

            migrationBuilder.DropIndex(
                name: "IX_OrderEmployees_EmployeeFromId",
                table: "OrderEmployees");

            migrationBuilder.DropColumn(
                name: "DateTo",
                table: "OrderEmployees");

            migrationBuilder.DropColumn(
                name: "EmployeeFromId",
                table: "OrderEmployees");

            migrationBuilder.RenameColumn(
                name: "EmployeeToId",
                table: "OrderEmployees",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderEmployees_EmployeeToId",
                table: "OrderEmployees",
                newName: "IX_OrderEmployees_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEmployees_AspNetUsers_EmployeeId",
                table: "OrderEmployees",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
