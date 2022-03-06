using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyElectricalShop.Infrastructure.Data.Migrations
{
    public partial class NewConnectionsCartLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartLines_Carts_CartId",
                table: "CartLines");

            migrationBuilder.AlterColumn<Guid>(
                name: "CartId",
                table: "CartLines",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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
                name: "FK_CartLines_Carts_CartId",
                table: "CartLines",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartLines_Carts_CartId1",
                table: "CartLines",
                column: "CartId1",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartLines_Carts_CartId",
                table: "CartLines");

            migrationBuilder.DropForeignKey(
                name: "FK_CartLines_Carts_CartId1",
                table: "CartLines");

            migrationBuilder.DropIndex(
                name: "IX_CartLines_CartId1",
                table: "CartLines");

            migrationBuilder.DropColumn(
                name: "CartId1",
                table: "CartLines");

            migrationBuilder.AlterColumn<Guid>(
                name: "CartId",
                table: "CartLines",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_CartLines_Carts_CartId",
                table: "CartLines",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }
    }
}
