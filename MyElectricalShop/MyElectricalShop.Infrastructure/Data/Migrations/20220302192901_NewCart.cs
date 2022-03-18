using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyElectricalShop.Infrastructure.Data.Migrations
{
    public partial class NewCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartLines_Carts_CartId1",
                table: "CartLines");

            migrationBuilder.DropIndex(
                name: "IX_CartLines_CartId1",
                table: "CartLines");

            migrationBuilder.DropColumn(
                name: "CartId1",
                table: "CartLines");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Carts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Carts");

            migrationBuilder.AddColumn<Guid>(
                name: "CartId1",
                table: "CartLines",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartLines_CartId1",
                table: "CartLines",
                column: "CartId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartLines_Carts_CartId1",
                table: "CartLines",
                column: "CartId1",
                principalTable: "Carts",
                principalColumn: "Id");
        }
    }
}
