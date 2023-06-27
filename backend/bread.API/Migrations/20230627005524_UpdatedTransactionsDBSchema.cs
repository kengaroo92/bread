using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bread.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTransactionsDBSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIncome",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "Income");

            migrationBuilder.AddColumn<decimal>(
                name: "Expense",
                table: "Transactions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextOccurranceDate",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Recurrence",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expense",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "NextOccurranceDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Recurrence",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Income",
                table: "Transactions",
                newName: "Amount");

            migrationBuilder.AddColumn<bool>(
                name: "IsIncome",
                table: "Transactions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
