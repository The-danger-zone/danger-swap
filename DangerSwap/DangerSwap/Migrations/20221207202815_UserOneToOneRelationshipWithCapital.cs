using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DangerSwap.Migrations
{
    public partial class UserOneToOneRelationshipWithCapital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Capitals",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CapitalId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "6fb2a157-9964-47f4-a61d-1cb092c26ee4", new DateTime(2022, 12, 7, 20, 28, 15, 168, DateTimeKind.Utc).AddTicks(400) });

            migrationBuilder.CreateIndex(
                name: "IX_Capitals_UserId",
                table: "Capitals",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Capitals_AspNetUsers_UserId",
                table: "Capitals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Capitals_AspNetUsers_UserId",
                table: "Capitals");

            migrationBuilder.DropIndex(
                name: "IX_Capitals_UserId",
                table: "Capitals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Capitals");

            migrationBuilder.DropColumn(
                name: "CapitalId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "01777d12-6529-4a1b-8816-a65005585ca5", new DateTime(2022, 12, 7, 20, 22, 2, 476, DateTimeKind.Utc).AddTicks(9625) });
        }
    }
}
