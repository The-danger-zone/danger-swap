using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DangerSwap.Migrations
{
    public partial class MakeUserCapitalOneToManyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Capitals_UserId",
                table: "Capitals");

            migrationBuilder.DropColumn(
                name: "CapitalId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "ff0c145a-a802-4ee3-8595-fd479849a38d", new DateTime(2022, 12, 20, 18, 17, 21, 39, DateTimeKind.Utc).AddTicks(5554) });

            migrationBuilder.CreateIndex(
                name: "IX_Capitals_UserId",
                table: "Capitals",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Capitals_UserId",
                table: "Capitals");

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
        }
    }
}
