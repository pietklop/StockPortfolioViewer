using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Net8_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Currencies_CurrencyKey",
                table: "Stocks");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyKey",
                table: "Stocks",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Currencies_CurrencyKey",
                table: "Stocks",
                column: "CurrencyKey",
                principalTable: "Currencies",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Currencies_CurrencyKey",
                table: "Stocks");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyKey",
                table: "Stocks",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Currencies_CurrencyKey",
                table: "Stocks",
                column: "CurrencyKey",
                principalTable: "Currencies",
                principalColumn: "Key");
        }
    }
}
