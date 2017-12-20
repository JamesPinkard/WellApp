using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WellApp.Data.Migrations
{
    public partial class RegionProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "Wells",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GCD",
                table: "Wells",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GMA",
                table: "Wells",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RWPA",
                table: "Wells",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RiverBasin",
                table: "Wells",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "County",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "GCD",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "GMA",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "RWPA",
                table: "Wells");

            migrationBuilder.DropColumn(
                name: "RiverBasin",
                table: "Wells");
        }
    }
}
