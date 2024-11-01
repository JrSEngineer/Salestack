using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Salestack.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingProductAndServiceTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalestackProduct_Company_CompanyId",
                table: "SalestackProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_SalestackService_Company_CompanyId",
                table: "SalestackService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalestackService",
                table: "SalestackService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalestackProduct",
                table: "SalestackProduct");

            migrationBuilder.RenameTable(
                name: "SalestackService",
                newName: "Service");

            migrationBuilder.RenameTable(
                name: "SalestackProduct",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_SalestackService_CompanyId",
                table: "Service",
                newName: "IX_Service_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_SalestackProduct_CompanyId",
                table: "Product",
                newName: "IX_Product_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service",
                table: "Service",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Company_CompanyId",
                table: "Product",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Company_CompanyId",
                table: "Service",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Company_CompanyId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Company_CompanyId",
                table: "Service");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Service",
                table: "Service");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Service",
                newName: "SalestackService");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "SalestackProduct");

            migrationBuilder.RenameIndex(
                name: "IX_Service_CompanyId",
                table: "SalestackService",
                newName: "IX_SalestackService_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CompanyId",
                table: "SalestackProduct",
                newName: "IX_SalestackProduct_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalestackService",
                table: "SalestackService",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalestackProduct",
                table: "SalestackProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalestackProduct_Company_CompanyId",
                table: "SalestackProduct",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalestackService_Company_CompanyId",
                table: "SalestackService",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
