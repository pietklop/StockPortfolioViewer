using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Edit_StockRetrieverCompatibility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCompatible",
                table: "StockRetrieverCompatibility",
                newName: "Compatibility");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Compatibility",
                table: "StockRetrieverCompatibility",
                newName: "IsCompatible");
        }
    }
}
