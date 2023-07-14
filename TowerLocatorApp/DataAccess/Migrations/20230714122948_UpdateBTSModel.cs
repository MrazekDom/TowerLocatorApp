using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace TowerLocatorApp.Migrations
{
    public partial class UpdateBTSModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "ActualTowerLocation",
                table: "BTSSet",
                type: "geometry",
                nullable: false);

            migrationBuilder.AddColumn<Point>(
                name: "MyLocationAtMeasurement",
                table: "BTSSet",
                type: "geometry",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualTowerLocation",
                table: "BTSSet");

            migrationBuilder.DropColumn(
                name: "MyLocationAtMeasurement",
                table: "BTSSet");
        }
    }
}
