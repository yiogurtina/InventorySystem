using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace inventory_accounting_system.Migrations
{
    public partial class AddColumnToTimeLast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_EventType_EventTypeId",
                table: "Event");

            migrationBuilder.DropTable(
                name: "EventType");

            migrationBuilder.RenameColumn(
                name: "EventTypeId",
                table: "Event",
                newName: "EventCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_EventTypeId",
                table: "Event",
                newName: "IX_Event_EventCategoryId");

            migrationBuilder.CreateTable(
                name: "EventCategory",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TimeLast = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategory", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Event_EventCategory_EventCategoryId",
                table: "Event",
                column: "EventCategoryId",
                principalTable: "EventCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_EventCategory_EventCategoryId",
                table: "Event");

            migrationBuilder.DropTable(
                name: "EventCategory");

            migrationBuilder.RenameColumn(
                name: "EventCategoryId",
                table: "Event",
                newName: "EventTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_EventCategoryId",
                table: "Event",
                newName: "IX_Event_EventTypeId");

            migrationBuilder.CreateTable(
                name: "EventType",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventType", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Event_EventType_EventTypeId",
                table: "Event",
                column: "EventTypeId",
                principalTable: "EventType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
