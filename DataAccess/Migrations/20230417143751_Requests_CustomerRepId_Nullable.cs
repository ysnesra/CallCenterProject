using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Requests_CustomerRepId_Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_CustomerReps_CustomerRepId",
                table: "Requests");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerRepId",
                table: "Requests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_CustomerReps_CustomerRepId",
                table: "Requests",
                column: "CustomerRepId",
                principalTable: "CustomerReps",
                principalColumn: "CustomerRepId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_CustomerReps_CustomerRepId",
                table: "Requests");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerRepId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_CustomerReps_CustomerRepId",
                table: "Requests",
                column: "CustomerRepId",
                principalTable: "CustomerReps",
                principalColumn: "CustomerRepId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
