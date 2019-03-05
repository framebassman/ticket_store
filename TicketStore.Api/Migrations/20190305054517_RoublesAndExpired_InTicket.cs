using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketStore.Api.Migrations
{
    public partial class RoublesAndExpired_InTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "expired",
                table: "tickets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "roubles",
                table: "tickets",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expired",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "roubles",
                table: "tickets");
        }
    }
}
