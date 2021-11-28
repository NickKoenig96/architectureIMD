using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.API.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "eventParticipants",
                table: "Event");

            migrationBuilder.CreateTable(
                name: "Enrolled",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    eventTitle = table.Column<string>(type: "TEXT", nullable: true),
                    userName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrolled", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrolled");

            migrationBuilder.AddColumn<string>(
                name: "eventParticipants",
                table: "Event",
                type: "TEXT",
                nullable: true);
        }
    }
}
