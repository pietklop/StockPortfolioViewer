using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Add_Stock_LastKnownValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PitStockValue_Stocks_StockId",
                table: "PitStockValue");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PitStockValue_StockValueId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PitStockValue",
                table: "PitStockValue");

            migrationBuilder.RenameTable(
                name: "PitStockValue",
                newName: "PitStockValues");

            migrationBuilder.RenameIndex(
                name: "IX_PitStockValue_StockId",
                table: "PitStockValues",
                newName: "IX_PitStockValues_StockId");

            migrationBuilder.AddColumn<int>(
                name: "LastKnownStockValueId",
                table: "Stocks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PitStockValues",
                table: "PitStockValues",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LastKnownStockValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StockValueId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastKnownStockValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LastKnownStockValue_PitStockValues_StockValueId",
                        column: x => x.StockValueId,
                        principalTable: "PitStockValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_LastKnownStockValueId",
                table: "Stocks",
                column: "LastKnownStockValueId");

            migrationBuilder.CreateIndex(
                name: "IX_LastKnownStockValue_StockValueId",
                table: "LastKnownStockValue",
                column: "StockValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_PitStockValues_Stocks_StockId",
                table: "PitStockValues",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_LastKnownStockValue_LastKnownStockValueId",
                table: "Stocks",
                column: "LastKnownStockValueId",
                principalTable: "LastKnownStockValue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PitStockValues_StockValueId",
                table: "Transactions",
                column: "StockValueId",
                principalTable: "PitStockValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PitStockValues_Stocks_StockId",
                table: "PitStockValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_LastKnownStockValue_LastKnownStockValueId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PitStockValues_StockValueId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "LastKnownStockValue");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_LastKnownStockValueId",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PitStockValues",
                table: "PitStockValues");

            migrationBuilder.DropColumn(
                name: "LastKnownStockValueId",
                table: "Stocks");

            migrationBuilder.RenameTable(
                name: "PitStockValues",
                newName: "PitStockValue");

            migrationBuilder.RenameIndex(
                name: "IX_PitStockValues_StockId",
                table: "PitStockValue",
                newName: "IX_PitStockValue_StockId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PitStockValue",
                table: "PitStockValue",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PitStockValue_Stocks_StockId",
                table: "PitStockValue",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PitStockValue_StockValueId",
                table: "Transactions",
                column: "StockValueId",
                principalTable: "PitStockValue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
