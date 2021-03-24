using Microsoft.EntityFrameworkCore.Migrations;

namespace WebSiteStatistics.DataLayer.Migrations
{
    public partial class EditCountryAddRegionNameCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionCode",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionName",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "RegionCode",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "RegionName",
                table: "Country");
        }
    }
}
