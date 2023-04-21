using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedFrozenByUserField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_BankAccounts_BankAccountId",
                table: "BankAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccount",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "FrozenByUser",
                table: "BankAccount");

            migrationBuilder.RenameTable(
                name: "BankAccount",
                newName: "BankAccountFrozenStatus");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_BankAccountId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccountFrozenStatus_BankAccounts_BankAccountId",
                table: "BankAccountFrozenStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccountFrozenStatus",
                table: "BankAccountFrozenStatus");

            migrationBuilder.RenameTable(
                name: "BankAccountFrozenStatus",
                newName: "BankAccount");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccountFrozenStatus_BankAccountId",
                table: "BankAccount",
                newName: "IX_BankAccount_BankAccountId");

            migrationBuilder.AddColumn<int>(
                name: "FrozenByUser",
                table: "BankAccount",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
    }
}
