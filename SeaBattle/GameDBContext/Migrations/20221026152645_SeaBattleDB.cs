using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameDBContext.Migrations
{
    public partial class SeaBattleDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BattlesPlayed = table.Column<int>(type: "int", nullable: false),
                    BattlesWon = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterId = table.Column<int>(type: "int", nullable: true),
                    FriendList = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RewardsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounts_Rewards_RewardsId",
                        column: x => x.RewardsId,
                        principalTable: "Rewards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentBattles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstPlayerId = table.Column<int>(type: "int", nullable: false),
                    SecondPlayerId = table.Column<int>(type: "int", nullable: false),
                    Move = table.Column<bool>(type: "bit", nullable: false),
                    FirstFieldData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondFieldData = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentBattles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CurrentBattles_Accounts_FirstPlayerId",
                        column: x => x.FirstPlayerId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrentBattles_Accounts_SecondPlayerId",
                        column: x => x.SecondPlayerId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Registrations_Accounts_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RegisterId",
                table: "Accounts",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RewardsId",
                table: "Accounts",
                column: "RewardsId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentBattles_FirstPlayerId",
                table: "CurrentBattles",
                column: "FirstPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentBattles_SecondPlayerId",
                table: "CurrentBattles",
                column: "SecondPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_PlayerId",
                table: "Registrations",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Registrations_RegisterId",
                table: "Accounts",
                column: "RegisterId",
                principalTable: "Registrations",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Registrations_RegisterId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "CurrentBattles");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Rewards");
        }
    }
}
