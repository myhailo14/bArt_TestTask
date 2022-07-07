using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.Migrations
{
    public partial class ModelsCreatingUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Accounts_AccountName",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Accounts_AccountName",
                table: "Incidents");

            migrationBuilder.AlterColumn<string>(
                name: "AccountName",
                table: "Incidents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AccountName",
                table: "Contacts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Accounts_AccountName",
                table: "Contacts",
                column: "AccountName",
                principalTable: "Accounts",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Accounts_AccountName",
                table: "Incidents",
                column: "AccountName",
                principalTable: "Accounts",
                principalColumn: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Accounts_AccountName",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Accounts_AccountName",
                table: "Incidents");

            migrationBuilder.AlterColumn<string>(
                name: "AccountName",
                table: "Incidents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountName",
                table: "Contacts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Accounts_AccountName",
                table: "Contacts",
                column: "AccountName",
                principalTable: "Accounts",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Accounts_AccountName",
                table: "Incidents",
                column: "AccountName",
                principalTable: "Accounts",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
