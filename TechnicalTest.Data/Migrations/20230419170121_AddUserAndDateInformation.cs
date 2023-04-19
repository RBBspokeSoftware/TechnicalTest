using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndDateInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserID",
                table: "Customers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Customers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserID",
                table: "Customers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserID",
                table: "Customers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "BankAccounts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserID",
                table: "BankAccounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "BankAccounts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserID",
                table: "BankAccounts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "BankAccounts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserID",
                table: "BankAccounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeletedByUserID",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "DeletedByUserID",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                table: "BankAccounts");
        }
    }
}
