using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopApplication.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_products_productId",
                table: "carts");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "carts",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_carts_productId",
                table: "carts",
                newName: "IX_carts_ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_products_ProductID",
                table: "carts",
                column: "ProductID",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_products_ProductID",
                table: "carts");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "carts",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_carts_ProductID",
                table: "carts",
                newName: "IX_carts_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_products_productId",
                table: "carts",
                column: "productId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
