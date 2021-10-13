using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    eventTitle = table.Column<string>(type: "TEXT", nullable: true),
                    eventDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    eventDescription = table.Column<string>(type: "TEXT", nullable: true),
                    eventAge = table.Column<int>(type: "INTEGER", nullable: false),
                    eventParticpants = table.Column<string>(type: "TEXT", nullable: true),
                    eventParticpantCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    userName = table.Column<string>(type: "TEXT", nullable: true),
                    userBirthdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    userEmail = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
