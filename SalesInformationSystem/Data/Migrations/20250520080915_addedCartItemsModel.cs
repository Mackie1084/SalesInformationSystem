using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesInformationSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedCartItemsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "InvoiceDetail");

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartPk);
                    table.ForeignKey(
                        name: "FK_CartItems_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            //migrationBuilder.CreateTable(
            //    name: "InvoiceDetail",
            //    columns: table => new
            //    {
            //        InvoiceDetailId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        InvoiceId = table.Column<int>(type: "int", nullable: false),
            //        ProductId = table.Column<int>(type: "int", nullable: false),
            //        Price = table.Column<double>(type: "float", nullable: false),
            //        Quantity = table.Column<int>(type: "int", nullable: false),
            //        TotalPrice = table.Column<double>(type: "float", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_InvoiceDetail", x => x.InvoiceDetailId);
            //        table.ForeignKey(
            //            name: "FK_InvoiceDetail_Invoice_InvoiceId",
            //            column: x => x.InvoiceId,
            //            principalTable: "Invoice",
            //            principalColumn: "InvoiceId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_InvoiceDetail_Product_ProductId",
            //            column: x => x.ProductId,
            //            principalTable: "Product",
            //            principalColumn: "ProductId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_InvoiceDetail_InvoiceId",
            //    table: "InvoiceDetail",
            //    column: "InvoiceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_InvoiceDetail_ProductId",
            //    table: "InvoiceDetail",
            //    column: "ProductId");
        }
    }
}
