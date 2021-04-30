using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Add_AreaShareTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AreaShare_Areas_AreaId",
                table: "AreaShare");

            migrationBuilder.DropForeignKey(
                name: "FK_AreaShare_Stocks_StockId",
                table: "AreaShare");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AreaShare",
                table: "AreaShare");

            migrationBuilder.RenameTable(
                name: "AreaShare",
                newName: "AreaShares");

            migrationBuilder.RenameIndex(
                name: "IX_AreaShare_StockId",
                table: "AreaShares",
                newName: "IX_AreaShares_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_AreaShare_AreaId",
                table: "AreaShares",
                newName: "IX_AreaShares_AreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AreaShares",
                table: "AreaShares",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AreaShares_Areas_AreaId",
                table: "AreaShares",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AreaShares_Stocks_StockId",
                table: "AreaShares",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AreaShares_Areas_AreaId",
                table: "AreaShares");

            migrationBuilder.DropForeignKey(
                name: "FK_AreaShares_Stocks_StockId",
                table: "AreaShares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AreaShares",
                table: "AreaShares");

            migrationBuilder.RenameTable(
                name: "AreaShares",
                newName: "AreaShare");

            migrationBuilder.RenameIndex(
                name: "IX_AreaShares_StockId",
                table: "AreaShare",
                newName: "IX_AreaShare_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_AreaShares_AreaId",
                table: "AreaShare",
                newName: "IX_AreaShare_AreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AreaShare",
                table: "AreaShare",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AreaShare_Areas_AreaId",
                table: "AreaShare",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AreaShare_Stocks_StockId",
                table: "AreaShare",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
