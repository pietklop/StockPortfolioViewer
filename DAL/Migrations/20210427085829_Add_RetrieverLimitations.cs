using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Add_RetrieverLimitations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CallsLastMonth",
                table: "DataRetrievers",
                newName: "Priority");

            migrationBuilder.CreateTable(
                name: "RetrieverLimitations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataRetrieverId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimespanType = table.Column<int>(type: "INTEGER", nullable: false),
                    Limit = table.Column<int>(type: "INTEGER", nullable: false),
                    RequestsDone = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetrieverLimitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetrieverLimitations_DataRetrievers_DataRetrieverId",
                        column: x => x.DataRetrieverId,
                        principalTable: "DataRetrievers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RetrieverLimitations_DataRetrieverId",
                table: "RetrieverLimitations",
                column: "DataRetrieverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RetrieverLimitations");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "DataRetrievers",
                newName: "CallsLastMonth");
        }
    }
}
