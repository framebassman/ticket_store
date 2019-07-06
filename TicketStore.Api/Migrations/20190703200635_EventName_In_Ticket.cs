using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketStore.Api.Migrations
{
    public partial class EventName_In_Ticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "event_name",
                table: "tickets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "event_name",
                table: "tickets");
        }
    }
}
