using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyElectricalShop.Infrastructure.Data.Migrations
{
    public partial class AddedUniqueIndexForCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");
        }
    }
}
