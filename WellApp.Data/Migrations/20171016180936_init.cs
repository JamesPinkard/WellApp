using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WellApp.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aquifers",
                columns: table => new
                {
                    AquiferID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AquiferCode = table.Column<string>(nullable: true),
                    AquiferCodeDescriprion = table.Column<string>(nullable: true),
                    AquiferName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aquifers", x => x.AquiferID);
                });

            migrationBuilder.CreateTable(
                name: "Wells",
                columns: table => new
                {
                    WellId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AquiferId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DrillingEndDate = table.Column<DateTime>(nullable: false),
                    GroundSurfaceElevation = table.Column<int>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    StateWellNumber = table.Column<string>(nullable: true),
                    WellDepth = table.Column<int>(nullable: false),
                    WellReportTrackingNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wells", x => x.WellId);
                    table.ForeignKey(
                        name: "FK_Wells_Aquifers_AquiferId",
                        column: x => x.AquiferId,
                        principalTable: "Aquifers",
                        principalColumn: "AquiferID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wells_AquiferId",
                table: "Wells",
                column: "AquiferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wells");

            migrationBuilder.DropTable(
                name: "Aquifers");
        }
    }
}
