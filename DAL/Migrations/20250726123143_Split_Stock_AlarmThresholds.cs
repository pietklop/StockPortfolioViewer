using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Split_Stock_AlarmThresholds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlarmCondition",
                table: "Stocks");

            migrationBuilder.AddColumn<double>(
                name: "AlarmUpperThreshold",
                table: "Stocks",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlarmUpperThreshold",
                table: "Stocks");

            migrationBuilder.AddColumn<int>(
                name: "AlarmCondition",
                table: "Stocks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
