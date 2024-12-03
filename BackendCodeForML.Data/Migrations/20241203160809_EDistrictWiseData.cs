using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendCodeForML.Data.Migrations
{
    /// <inheritdoc />
    public partial class EDistrictWiseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EDistrictWiseData");

            migrationBuilder.CreateTable(
                name: "EDistrict",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rainfall = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvgTemp = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RelativeHumidity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoilTemp = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sand = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PHLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Phosphorus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Potassium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Clay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EDistrict", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EDistrict");

            migrationBuilder.CreateTable(
                name: "EDistrictWiseData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvgTemp = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Clay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PHLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Phosphorus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Potassium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductionArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rainfall = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RelativeHumidity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sand = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoilTemp = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EDistrictWiseData", x => x.Id);
                });
        }
    }
}
