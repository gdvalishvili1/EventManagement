using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Infrastructure.Migrations
{
    public partial class addaggregatetypename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                schema: "EventStore",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                schema: "EventStore",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "AggregateName",
                schema: "EventStore",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                schema: "EventStore",
                table: "Events",
                columns: new[] { "AggregateRootId", "AggregateName", "Version" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                schema: "EventStore",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AggregateName",
                schema: "EventStore",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                schema: "EventStore",
                table: "Events",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                schema: "EventStore",
                table: "Events",
                columns: new[] { "AggregateRootId", "EventName", "Version" });
        }
    }
}
