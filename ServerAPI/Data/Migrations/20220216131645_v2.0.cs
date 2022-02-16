using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerAPI.Migrations
{
    public partial class v20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Recurring",
                table: "Events",
                newName: "IterationsFinished");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Recipients = table.Column<string>(type: "TEXT", nullable: true),
                    ClientPassword = table.Column<string>(type: "TEXT", nullable: true),
                    AdminPassword = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    revisionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    revisionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ServerName = table.Column<string>(type: "TEXT", nullable: true),
                    Port = table.Column<int>(type: "INTEGER", nullable: false),
                    HostEmailUsername = table.Column<string>(type: "TEXT", nullable: true),
                    HostEmailPassword = table.Column<string>(type: "TEXT", nullable: true),
                    NotificationTime = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.revisionID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.RenameColumn(
                name: "IterationsFinished",
                table: "Events",
                newName: "Recurring");
        }
    }
}
