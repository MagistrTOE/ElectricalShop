using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyElectricalShop.Infrastructure.Data.Migrations
{
    public partial class AddedUniqueIndexForCartLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CartLines_ProductId_CartId",
                table: "CartLines",
                columns: new[] { "ProductId", "CartId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
