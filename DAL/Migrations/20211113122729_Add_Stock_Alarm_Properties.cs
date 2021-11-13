using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Add_Stock_Alarm_Properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlarmCondition",
                table: "Stocks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "AlarmThreshold",
                table: "Stocks",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Stocks",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlarmCondition",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "AlarmThreshold",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Stocks");
        }
    }
}
