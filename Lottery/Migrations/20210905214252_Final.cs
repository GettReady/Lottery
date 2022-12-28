using Microsoft.EntityFrameworkCore.Migrations;

namespace Lottery.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Raffle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Raffle",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Raffle",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrivateRafflesUserList");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Raffle");

            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Raffle");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Raffle");
        }
    }
}
