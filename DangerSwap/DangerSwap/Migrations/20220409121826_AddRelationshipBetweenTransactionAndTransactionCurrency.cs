using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DangerSwap.Migrations
{
    public partial class AddRelationshipBetweenTransactionAndTransactionCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TransactionCurrencies_TransactionId",
                table: "TransactionCurrencies");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCurrencies_TransactionId",
                table: "TransactionCurrencies",
                column: "TransactionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TransactionCurrencies_TransactionId",
                table: "TransactionCurrencies");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCurrencies_TransactionId",
                table: "TransactionCurrencies",
                column: "TransactionId");
        }
    }
}
