using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DangerSwap.Migrations
{
    public partial class MakeRateNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Rate_RateId",
                table: "Currencies");

            migrationBuilder.AlterColumn<string>(
                name: "RateId",
                table: "Currencies",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Rate_RateId",
                table: "Currencies",
                column: "RateId",
                principalTable: "Rate",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Rate_RateId",
                table: "Currencies");

            migrationBuilder.AlterColumn<string>(
                name: "RateId",
                table: "Currencies",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Rate_RateId",
                table: "Currencies",
                column: "RateId",
                principalTable: "Rate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
