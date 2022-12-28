using Microsoft.EntityFrameworkCore.Migrations;

namespace Lottery.Migrations
{
    public partial class PrizeWinner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Prize",
                table: "Prize");

            migrationBuilder.RenameColumn(
                name: "Placement",
                table: "Prize",
                newName: "WinnerId");

            migrationBuilder.AddColumn<int>(
                name: "Place",
                table: "Prize",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prize",
                table: "Prize",
                columns: new[] { "RaffleId", "Place" });

            migrationBuilder.CreateIndex(
                name: "IX_Prize_WinnerId",
                table: "Prize",
                column: "WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prize_User_WinnerId",
                table: "Prize",
                column: "WinnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prize_User_WinnerId",
                table: "Prize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prize",
                table: "Prize");

            migrationBuilder.DropIndex(
                name: "IX_Prize_WinnerId",
                table: "Prize");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "Prize");

            migrationBuilder.RenameColumn(
                name: "WinnerId",
                table: "Prize",
                newName: "Placement");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prize",
                table: "Prize",
                columns: new[] { "RaffleId", "Placement" });
        }
    }
}
