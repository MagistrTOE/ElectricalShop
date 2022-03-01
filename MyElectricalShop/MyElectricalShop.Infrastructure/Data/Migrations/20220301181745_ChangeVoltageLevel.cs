using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyElectricalShop.Infrastructure.Data.Migrations
{
    public partial class ChangeVoltageLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "VoltageLevels");

            migrationBuilder.AddColumn<int>(
                name: "MaxLevel",
                table: "VoltageLevels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinLevel",
                table: "VoltageLevels",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxLevel",
                table: "VoltageLevels");

            migrationBuilder.DropColumn(
                name: "MinLevel",
                table: "VoltageLevels");

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "VoltageLevels",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
