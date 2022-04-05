using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DangerSwap.Migrations
{
    public partial class AddCurrencyRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RateId",
                table: "Currencies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Rate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    RateUsd = table.Column<double>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_RateId",
                table: "Currencies",
                column: "RateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Rate_RateId",
                table: "Currencies",
                column: "RateId",
                principalTable: "Rate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Rate_RateId",
                table: "Currencies");

            migrationBuilder.DropTable(
                name: "Rate");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_RateId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "RateId",
                table: "Currencies");
        }
    }
}
