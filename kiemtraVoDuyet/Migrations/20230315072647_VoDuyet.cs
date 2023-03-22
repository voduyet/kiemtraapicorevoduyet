using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kiemtraVoDuyet.Migrations
{
    /// <inheritdoc />
    public partial class VoDuyet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Images_Images_ImageId",
                table: "Product_Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Images_Products_ProductId",
                table: "Product_Images");

            migrationBuilder.DropIndex(
                name: "IX_Product_Images_ImageId",
                table: "Product_Images");

            migrationBuilder.DropIndex(
                name: "IX_Product_Images_ProductId",
                table: "Product_Images");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Product_Images");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Product_Images");
                
            migrationBuilder.CreateIndex(
                name: "IX_Product_Images_IdImage",
                table: "Product_Images",
                column: "IdImage");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Images_IdProduct",
                table: "Product_Images",
                column: "IdProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Images_Images_IdImage",
                table: "Product_Images",
                column: "IdImage",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Images_Products_IdProduct",
                table: "Product_Images",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Images_Images_IdImage",
                table: "Product_Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Images_Products_IdProduct",
                table: "Product_Images");

            migrationBuilder.DropIndex(
                name: "IX_Product_Images_IdImage",
                table: "Product_Images");

            migrationBuilder.DropIndex(
                name: "IX_Product_Images_IdProduct",
                table: "Product_Images");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Product_Images",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Product_Images",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Images_ImageId",
                table: "Product_Images",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Images_ProductId",
                table: "Product_Images",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Images_Images_ImageId",
                table: "Product_Images",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Images_Products_ProductId",
                table: "Product_Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
