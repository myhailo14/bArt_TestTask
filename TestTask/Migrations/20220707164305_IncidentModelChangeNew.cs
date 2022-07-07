using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.Migrations
{
    public partial class IncidentModelChangeNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Accounts_AccountName",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "AccountName",
                table: "Contacts",
                newName: "AccountName1");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_AccountName",
                table: "Contacts",
                newName: "IX_Contacts_AccountName1");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Accounts_AccountName1",
                table: "Contacts",
                column: "AccountName1",
                principalTable: "Accounts",
                principalColumn: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Accounts_AccountName1",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "AccountName1",
                table: "Contacts",
                newName: "AccountName");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_AccountName1",
                table: "Contacts",
                newName: "IX_Contacts_AccountName");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Accounts_AccountName",
                table: "Contacts",
                column: "AccountName",
                principalTable: "Accounts",
                principalColumn: "Name");
        }
    }
}
