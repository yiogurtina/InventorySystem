using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inventory_accounting_system.Migrations
{
    public partial class AddNewColumnSerialNumToAssets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SerialNum",
                table: "Assets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNum",
                table: "Assets");
        }
    }
}
