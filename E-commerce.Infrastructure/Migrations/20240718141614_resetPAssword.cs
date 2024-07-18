using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class resetPAssword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ConfirmationToken",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordToken",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordTokenExpiration",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ConfirmationToken",
                table: "Users",
                column: "ConfirmationToken",
                unique: true,
                filter: "[ConfirmationToken] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ResetPasswordToken",
                table: "Users",
                column: "ResetPasswordToken",
                unique: true,
                filter: "[ResetPasswordToken] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_ConfirmationToken",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ResetPasswordToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ResetPasswordToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ResetPasswordTokenExpiration",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmationToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
