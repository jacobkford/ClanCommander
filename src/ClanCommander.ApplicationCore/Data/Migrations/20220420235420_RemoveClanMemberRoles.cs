using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClanCommander.ApplicationCore.Data.Migrations
{
    public partial class RemoveClanMemberRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuildClanMemberRole",
                schema: "discord_coc");

            migrationBuilder.DropSequence(
                name: "guild_clan_member_role_seq",
                schema: "discord_coc");

            migrationBuilder.DropColumn(
                name: "ClanRole",
                schema: "discord_coc",
                table: "GuildClanMember");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "guild_clan_member_role_seq",
                schema: "discord_coc",
                incrementBy: 10);

            migrationBuilder.AddColumn<int>(
                name: "ClanRole",
                schema: "discord_coc",
                table: "GuildClanMember",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GuildClanMemberRole",
                schema: "discord_coc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    DiscordRoleId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    GuildId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    InGameRole = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildClanMemberRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuildClanMemberRole_RegisteredDiscordGuild_GuildId",
                        column: x => x.GuildId,
                        principalSchema: "discord",
                        principalTable: "RegisteredDiscordGuild",
                        principalColumn: "GuildId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuildClanMemberRole_GuildId_InGameRole",
                schema: "discord_coc",
                table: "GuildClanMemberRole",
                columns: new[] { "GuildId", "InGameRole" },
                unique: true);
        }
    }
}
