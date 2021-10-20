using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.API.Migrations
{
    public partial class secondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "eventParticpants",
                table: "Event",
                newName: "eventParticipants");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "eventParticipants",
                table: "Event",
                newName: "eventParticpants");
        }
    }
}
