using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Salestack.Migrations
{
    /// <inheritdoc />
    public partial class AddingCompanyConstraintsForCNPJ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Company_Cnpj",
                table: "Company",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_PhoneNumber",
                table: "Company",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Company_Cnpj",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_PhoneNumber",
                table: "Company");
        }
    }
}
