using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Salestack.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingCustomerIdPropertyToOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SalestackOrderId",
                table: "Service",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SalestackOrderId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Order",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Service_SalestackOrderId",
                table: "Service",
                column: "SalestackOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SalestackOrderId",
                table: "Product",
                column: "SalestackOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Order_SalestackOrderId",
                table: "Product",
                column: "SalestackOrderId",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Order_SalestackOrderId",
                table: "Service",
                column: "SalestackOrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Order_SalestackOrderId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Order_SalestackOrderId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_SalestackOrderId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Product_SalestackOrderId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SalestackOrderId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "SalestackOrderId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Order");
        }
    }
}
