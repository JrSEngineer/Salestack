using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Salestack.Migrations
{
    /// <inheritdoc />
    public partial class AddingAuthentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Seller",
                newName: "AuthenticationEmail");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Manager",
                newName: "AuthenticationEmail");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Director",
                newName: "AuthenticationEmail");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthenticationUserId",
                table: "Seller",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AuthenticationUserId",
                table: "Manager",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AuthenticationUserId",
                table: "Director",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Authentication",
                columns: table => new
                {
                    Email = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Occupation = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authentication", x => new { x.UserId, x.Email });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seller_AuthenticationUserId_AuthenticationEmail",
                table: "Seller",
                columns: new[] { "AuthenticationUserId", "AuthenticationEmail" });

            migrationBuilder.CreateIndex(
                name: "IX_Manager_AuthenticationUserId_AuthenticationEmail",
                table: "Manager",
                columns: new[] { "AuthenticationUserId", "AuthenticationEmail" });

            migrationBuilder.CreateIndex(
                name: "IX_Director_AuthenticationUserId_AuthenticationEmail",
                table: "Director",
                columns: new[] { "AuthenticationUserId", "AuthenticationEmail" });

            migrationBuilder.AddForeignKey(
                name: "FK_Director_Authentication_AuthenticationUserId_Authentication~",
                table: "Director",
                columns: new[] { "AuthenticationUserId", "AuthenticationEmail" },
                principalTable: "Authentication",
                principalColumns: new[] { "UserId", "Email" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Manager_Authentication_AuthenticationUserId_AuthenticationE~",
                table: "Manager",
                columns: new[] { "AuthenticationUserId", "AuthenticationEmail" },
                principalTable: "Authentication",
                principalColumns: new[] { "UserId", "Email" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seller_Authentication_AuthenticationUserId_AuthenticationEm~",
                table: "Seller",
                columns: new[] { "AuthenticationUserId", "AuthenticationEmail" },
                principalTable: "Authentication",
                principalColumns: new[] { "UserId", "Email" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Director_Authentication_AuthenticationUserId_Authentication~",
                table: "Director");

            migrationBuilder.DropForeignKey(
                name: "FK_Manager_Authentication_AuthenticationUserId_AuthenticationE~",
                table: "Manager");

            migrationBuilder.DropForeignKey(
                name: "FK_Seller_Authentication_AuthenticationUserId_AuthenticationEm~",
                table: "Seller");

            migrationBuilder.DropTable(
                name: "Authentication");

            migrationBuilder.DropIndex(
                name: "IX_Seller_AuthenticationUserId_AuthenticationEmail",
                table: "Seller");

            migrationBuilder.DropIndex(
                name: "IX_Manager_AuthenticationUserId_AuthenticationEmail",
                table: "Manager");

            migrationBuilder.DropIndex(
                name: "IX_Director_AuthenticationUserId_AuthenticationEmail",
                table: "Director");

            migrationBuilder.DropColumn(
                name: "AuthenticationUserId",
                table: "Seller");

            migrationBuilder.DropColumn(
                name: "AuthenticationUserId",
                table: "Manager");

            migrationBuilder.DropColumn(
                name: "AuthenticationUserId",
                table: "Director");

            migrationBuilder.RenameColumn(
                name: "AuthenticationEmail",
                table: "Seller",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "AuthenticationEmail",
                table: "Manager",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "AuthenticationEmail",
                table: "Director",
                newName: "Email");
        }
    }
}
