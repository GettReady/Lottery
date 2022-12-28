using Microsoft.EntityFrameworkCore.Migrations;

namespace Lottery.Migrations
{
    public partial class Final2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "PrivateRafflesUserList");

            migrationBuilder.AddColumn<string>(
                name: "PrivateParticipants",
                table: "Raffle",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrivateParticipants",
                table: "Raffle");

            migrationBuilder.CreateTable(
                name: "PrivateRafflesUserList",
                columns: table => new
                {
                    RaffleId = table.Column<int>(type: "int", nullable: false),
                    ParticipantName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateRafflesUserList", x => new { x.RaffleId, x.ParticipantName });
                    table.ForeignKey(
                        name: "FK_PrivateRafflesUserList_Raffle_RaffleId",
                        column: x => x.RaffleId,
                        principalTable: "Raffle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
