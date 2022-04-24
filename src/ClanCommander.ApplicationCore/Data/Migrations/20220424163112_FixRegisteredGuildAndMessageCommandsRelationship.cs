using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClanCommander.ApplicationCore.Data.Migrations
{
    public partial class FixRegisteredGuildAndMessageCommandsRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuildMessageCommands_RegisteredDiscordGuild_GuildId",
                schema: "discord",
                table: "GuildMessageCommands");

            migrationBuilder.DropIndex(
                name: "IX_GuildMessageCommands_GuildId",
                schema: "discord",
                table: "GuildMessageCommands");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GuildMessageCommands_GuildId",
                schema: "discord",
                table: "GuildMessageCommands",
                column: "GuildId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuildMessageCommands_RegisteredDiscordGuild_GuildId",
                schema: "discord",
                table: "GuildMessageCommands",
                column: "GuildId",
                principalSchema: "discord",
                principalTable: "RegisteredDiscordGuild",
                principalColumn: "GuildId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
