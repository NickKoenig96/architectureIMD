using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.API.Migrations
{
    public partial class enrollmigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "eventEnrolled",
                table: "Event",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "eventEnrolled",
                table: "Event");
        }
    }
}
