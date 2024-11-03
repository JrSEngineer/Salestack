using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Salestack.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingStatusPropertyToVisitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Visit");

            migrationBuilder.DropColumn(
                name: "InProgress",
                table: "Visit");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Visit",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Visit");

            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "Visit",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InProgress",
                table: "Visit",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
