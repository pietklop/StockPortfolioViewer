using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Add_DataRetriever : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataRetrievers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    BaseUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Key = table.Column<string>(type: "TEXT", nullable: true),
                    LastRequest = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CallsLastMonth = table.Column<int>(type: "INTEGER", nullable: false),
                    LastRequestQuery = table.Column<string>(type: "TEXT", nullable: true),
                    LastResponseData = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataRetrievers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockRetrieverCompatibility",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataRetrieverId = table.Column<int>(type: "INTEGER", nullable: false),
                    StockRef = table.Column<string>(type: "TEXT", nullable: true),
                    IsCompatible = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockRetrieverCompatibility", x => new { x.StockId, x.DataRetrieverId });
                    table.ForeignKey(
                        name: "FK_StockRetrieverCompatibility_DataRetrievers_DataRetrieverId",
                        column: x => x.DataRetrieverId,
                        principalTable: "DataRetrievers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockRetrieverCompatibility_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockRetrieverCompatibility_DataRetrieverId",
                table: "StockRetrieverCompatibility",
                column: "DataRetrieverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockRetrieverCompatibility");

            migrationBuilder.DropTable(
                name: "DataRetrievers");
        }
    }
}
