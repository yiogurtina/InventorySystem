using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inventory_accounting_system.Migrations
{
    public partial class CreateColumnIsActiveInAssets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Assets",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Assets");
        }
    }
}
