using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Add_PitStockValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NativePrice",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserPrice",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "StockValueId",
                table: "Transactions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PitStockValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StockId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NativePrice = table.Column<double>(type: "REAL", nullable: false),
                    UserPrice = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PitStockValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PitStockValue_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_StockValueId",
                table: "Transactions",
                column: "StockValueId");

            migrationBuilder.CreateIndex(
                name: "IX_PitStockValue_StockId",
                table: "PitStockValue",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PitStockValue_StockValueId",
                table: "Transactions",
                column: "StockValueId",
                principalTable: "PitStockValue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PitStockValue_StockValueId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "PitStockValue");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_StockValueId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "StockValueId",
                table: "Transactions");

            migrationBuilder.AddColumn<double>(
                name: "NativePrice",
                table: "Transactions",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "UserPrice",
                table: "Transactions",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
