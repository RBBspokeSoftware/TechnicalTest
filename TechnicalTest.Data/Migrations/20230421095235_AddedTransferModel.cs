using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTransferModel : Migration
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

            migrationBuilder.RenameTable(
                name: "BankAccountFrozenStatus",
                newName: "BankAccountFrozenStatuses");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccountFrozenStatus_BankAccountId",
                table: "BankAccountFrozenStatuses",
                newName: "IX_BankAccountFrozenStatuses_BankAccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccountFrozenStatuses",
                table: "BankAccountFrozenStatuses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BankAccountTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FromBankAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToBankAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Reference = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountTransfers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccountFrozenStatuses_BankAccounts_BankAccountId",
                table: "BankAccountFrozenStatuses",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccountFrozenStatuses_BankAccounts_BankAccountId",
                table: "BankAccountFrozenStatuses");

            migrationBuilder.DropTable(
                name: "BankAccountTransfers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccountFrozenStatuses",
                table: "BankAccountFrozenStatuses");

            migrationBuilder.RenameTable(
                name: "BankAccountFrozenStatuses",
                newName: "BankAccountFrozenStatus");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccountFrozenStatuses_BankAccountId",
                table: "BankAccountFrozenStatus",
                newName: "IX_BankAccountFrozenStatus_BankAccountId");

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
