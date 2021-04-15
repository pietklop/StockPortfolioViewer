using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Add_LastKnownStockValue_LastUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LastKnownStockValue_PitStockValues_StockValueId",
                table: "LastKnownStockValue");

            migrationBuilder.AlterColumn<int>(
                name: "StockValueId",
                table: "LastKnownStockValue",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "LastKnownStockValue",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_LastKnownStockValue_PitStockValues_StockValueId",
                table: "LastKnownStockValue",
                column: "StockValueId",
                principalTable: "PitStockValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LastKnownStockValue_PitStockValues_StockValueId",
                table: "LastKnownStockValue");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "LastKnownStockValue");

            migrationBuilder.AlterColumn<int>(
                name: "StockValueId",
                table: "LastKnownStockValue",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_LastKnownStockValue_PitStockValues_StockValueId",
                table: "LastKnownStockValue",
                column: "StockValueId",
                principalTable: "PitStockValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
