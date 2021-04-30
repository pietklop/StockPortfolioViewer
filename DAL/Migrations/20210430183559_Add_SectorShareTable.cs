using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Add_SectorShareTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectorShare_Sectors_SectorId",
                table: "SectorShare");

            migrationBuilder.DropForeignKey(
                name: "FK_SectorShare_Stocks_StockId",
                table: "SectorShare");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectorShare",
                table: "SectorShare");

            migrationBuilder.RenameTable(
                name: "SectorShare",
                newName: "SectorShares");

            migrationBuilder.RenameIndex(
                name: "IX_SectorShare_StockId",
                table: "SectorShares",
                newName: "IX_SectorShares_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_SectorShare_SectorId",
                table: "SectorShares",
                newName: "IX_SectorShares_SectorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectorShares",
                table: "SectorShares",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SectorShares_Sectors_SectorId",
                table: "SectorShares",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SectorShares_Stocks_StockId",
                table: "SectorShares",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectorShares_Sectors_SectorId",
                table: "SectorShares");

            migrationBuilder.DropForeignKey(
                name: "FK_SectorShares_Stocks_StockId",
                table: "SectorShares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectorShares",
                table: "SectorShares");

            migrationBuilder.RenameTable(
                name: "SectorShares",
                newName: "SectorShare");

            migrationBuilder.RenameIndex(
                name: "IX_SectorShares_StockId",
                table: "SectorShare",
                newName: "IX_SectorShare_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_SectorShares_SectorId",
                table: "SectorShare",
                newName: "IX_SectorShare_SectorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectorShare",
                table: "SectorShare",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SectorShare_Sectors_SectorId",
                table: "SectorShare",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SectorShare_Stocks_StockId",
                table: "SectorShare",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
