using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditedModelDataFieldNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccountFrozenStatus_BankAccounts_BankAccountId",
                table: "BankAccountFrozenStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccountFrozenStatus",
                table: "BankAccountFrozenStatus");

            migrationBuilder.DropColumn(
                name: "IsFrozen",
                table: "BankAccountFrozenStatus");

            migrationBuilder.RenameTable(
                name: "BankAccountFrozenStatus",
                newName: "BankAccount");

            migrationBuilder.RenameColumn(
                name: "UpdatedByUserID",
                table: "Users",
                newName: "UpdatedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserID",
                table: "Users",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserID",
                table: "Users",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByUserID",
                table: "Customers",
                newName: "UpdatedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserID",
                table: "Customers",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserID",
                table: "Customers",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedByUserID",
                table: "BankAccounts",
                newName: "UpdatedByUserId");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserID",
                table: "BankAccounts",
                newName: "DeletedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserID",
                table: "BankAccounts",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserID",
                table: "BankAccount",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccountFrozenStatus_BankAccountId",
                table: "BankAccount",
                newName: "IX_BankAccount_BankAccountId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "BankAccount",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "BankAccount",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccount",
                table: "BankAccount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_BankAccounts_BankAccountId",
                table: "BankAccount",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_BankAccounts_BankAccountId",
                table: "BankAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccount",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BankAccount");

            migrationBuilder.RenameTable(
                name: "BankAccount",
                newName: "BankAccountFrozenStatus");

            migrationBuilder.RenameColumn(
                name: "UpdatedByUserId",
                table: "Users",
                newName: "UpdatedByUserID");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "Users",
                newName: "DeletedByUserID");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Users",
                newName: "CreatedByUserID");

            migrationBuilder.RenameColumn(
                name: "UpdatedByUserId",
                table: "Customers",
                newName: "UpdatedByUserID");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "Customers",
                newName: "DeletedByUserID");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Customers",
                newName: "CreatedByUserID");

            migrationBuilder.RenameColumn(
                name: "UpdatedByUserId",
                table: "BankAccounts",
                newName: "UpdatedByUserID");

            migrationBuilder.RenameColumn(
                name: "DeletedByUserId",
                table: "BankAccounts",
                newName: "DeletedByUserID");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "BankAccounts",
                newName: "CreatedByUserID");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "BankAccountFrozenStatus",
                newName: "CreatedByUserID");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_BankAccountId",
                table: "BankAccountFrozenStatus",
                newName: "IX_BankAccountFrozenStatus_BankAccountId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFrozen",
                table: "BankAccountFrozenStatus",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccountFrozenStatus",
                table: "BankAccountFrozenStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccountFrozenStatus_BankAccounts_BankAccountId",
                table: "BankAccountFrozenStatus",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
