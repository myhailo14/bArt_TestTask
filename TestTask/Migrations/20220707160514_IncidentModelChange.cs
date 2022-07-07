using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.Migrations
{
    public partial class IncidentModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Incidents_IncidentName",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "IncidentName",
                table: "Accounts",
                newName: "IncidentName1");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_IncidentName",
                table: "Accounts",
                newName: "IX_Accounts_IncidentName1");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Incidents_IncidentName1",
                table: "Accounts",
                column: "IncidentName1",
                principalTable: "Incidents",
                principalColumn: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Incidents_IncidentName1",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "IncidentName1",
                table: "Accounts",
                newName: "IncidentName");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_IncidentName1",
                table: "Accounts",
                newName: "IX_Accounts_IncidentName");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Incidents_IncidentName",
                table: "Accounts",
                column: "IncidentName",
                principalTable: "Incidents",
                principalColumn: "Name");
        }
    }
}
