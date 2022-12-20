using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DangerSwap.Migrations
{
    public partial class AddCapitalModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Capitals",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CurrencyId = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capitals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Capitals_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "01777d12-6529-4a1b-8816-a65005585ca5", new DateTime(2022, 12, 7, 20, 22, 2, 476, DateTimeKind.Utc).AddTicks(9625) });

            migrationBuilder.CreateIndex(
                name: "IX_Capitals_CurrencyId",
                table: "Capitals",
                column: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Capitals");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "b050a2e3-a1c3-4c6c-a187-b82cf6ddfba8", new DateTime(2022, 4, 26, 22, 54, 42, 890, DateTimeKind.Utc).AddTicks(5013) });
        }
    }
}
