using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TowerLocatorApp.Migrations
{
    public partial class BaseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Line = table.Column<LineString>(type: "geometry", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BTSSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mcc = table.Column<double>(type: "double precision", nullable: false),
                    mnc = table.Column<double>(type: "double precision", nullable: false),
                    lac = table.Column<double>(type: "double precision", nullable: false),
                    cell_id = table.Column<double>(type: "double precision", nullable: false),
                    short_cell_id = table.Column<double>(type: "double precision", nullable: true),
                    rnc = table.Column<double>(type: "double precision", nullable: true),
                    psc = table.Column<double>(type: "double precision", nullable: true),
                    asu = table.Column<double>(type: "double precision", nullable: false),
                    dbm = table.Column<double>(type: "double precision", nullable: false),
                    ta = table.Column<double>(type: "double precision", nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lon = table.Column<double>(type: "double precision", nullable: false),
                    accuracy = table.Column<double>(type: "double precision", nullable: false),
                    speed = table.Column<double>(type: "double precision", nullable: false),
                    bearing = table.Column<double>(type: "double precision", nullable: false),
                    altitude = table.Column<double>(type: "double precision", nullable: false),
                    measured_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    net_type = table.Column<string>(type: "text", nullable: false),
                    neighboring = table.Column<bool>(type: "boolean", nullable: false),
                    discovered_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    device = table.Column<string>(type: "text", nullable: false),
                    rsrp = table.Column<double>(type: "double precision", nullable: true),
                    rsrq = table.Column<double>(type: "double precision", nullable: true),
                    rssi = table.Column<double>(type: "double precision", nullable: false),
                    rssnr = table.Column<string>(type: "text", nullable: false),
                    cqi = table.Column<double>(type: "double precision", nullable: true),
                    rscp = table.Column<string>(type: "text", nullable: false),
                    csi_rsrp = table.Column<string>(type: "text", nullable: true),
                    csi_rsrq = table.Column<string>(type: "text", nullable: true),
                    csi_sinr = table.Column<string>(type: "text", nullable: true),
                    ss_rsrp = table.Column<string>(type: "text", nullable: true),
                    ss_rsrq = table.Column<string>(type: "text", nullable: true),
                    ss_sinr = table.Column<string>(type: "text", nullable: true),
                    cdma_dbm = table.Column<string>(type: "text", nullable: true),
                    cdma_ecio = table.Column<string>(type: "text", nullable: true),
                    evdo_dbm = table.Column<string>(type: "text", nullable: true),
                    evdo_ecio = table.Column<string>(type: "text", nullable: true),
                    evdo_snr = table.Column<string>(type: "text", nullable: true),
                    ec_no = table.Column<string>(type: "text", nullable: true),
                    arfcn = table.Column<string>(type: "text", nullable: true),
                    RouteId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BTSSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BTSSet_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MapPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Coordinates = table.Column<Point>(type: "geometry", nullable: false),
                    Elevation = table.Column<double>(type: "double precision", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RouteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapPoints_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BTSSet_RouteId",
                table: "BTSSet",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_MapPoints_RouteId",
                table: "MapPoints",
                column: "RouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BTSSet");

            migrationBuilder.DropTable(
                name: "MapPoints");

            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
