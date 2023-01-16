using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bokningsappen.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Weekdays");

            migrationBuilder.AddColumn<int>(
                name: "WeekdayId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_WeekdayId",
                table: "Rooms",
                column: "WeekdayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Weekdays_WeekdayId",
                table: "Rooms",
                column: "WeekdayId",
                principalTable: "Weekdays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
