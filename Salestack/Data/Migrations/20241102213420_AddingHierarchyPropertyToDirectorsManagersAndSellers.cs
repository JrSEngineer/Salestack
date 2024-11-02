using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Salestack.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingHierarchyPropertyToDirectorsManagersAndSellers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Hierarchy",
                table: "Seller",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hierarchy",
                table: "Manager",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hierarchy",
                table: "Director",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hierarchy",
                table: "Seller");

            migrationBuilder.DropColumn(
                name: "Hierarchy",
                table: "Manager");

            migrationBuilder.DropColumn(
                name: "Hierarchy",
                table: "Director");
        }
    }
}
