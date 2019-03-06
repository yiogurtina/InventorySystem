using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inventory_accounting_system.Migrations
{
    public partial class addColumnDaysBeforeAlert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BeforeAlertDays",
                table: "AssetEvents",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeforeAlertDays",
                table: "AssetEvents");
        }
    }
}
