using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClanCommander.ApplicationCore.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "discord_coc");

            migrationBuilder.EnsureSchema(
                name: "discord");

            migrationBuilder.CreateSequence(
                name: "coc_account_seq",
                schema: "discord_coc",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "discord_user_seq",
                schema: "discord",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "guild_clan_member_role_seq",
                schema: "discord_coc",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "guild_clan_seq",
                schema: "discord_coc",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "guild_message_commands_seq",
                schema: "discord",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "registered_discord_guild_seq",
                schema: "discord",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "DiscordUser",
                schema: "discord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<ulong>(type: "numeric(20,0)", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscordUser", x => x.Id);
                    table.UniqueConstraint("AK_DiscordUser_UserId", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredDiscordGuild",
                schema: "discord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    GuildId = table.Column<ulong>(type: "numeric(20,0)", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<ulong>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredDiscordGuild", x => x.Id);
                    table.UniqueConstraint("AK_RegisteredDiscordGuild_GuildId", x => x.GuildId);
                });

            migrationBuilder.CreateTable(
                name: "ClashOfClansAccount",
                schema: "discord_coc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    AccountId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<ulong>(type: "numeric(20,0)", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClashOfClansAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClashOfClansAccount_DiscordUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "discord",
                        principalTable: "DiscordUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuildClan",
                schema: "discord_coc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ClanId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    GuildId = table.Column<ulong>(type: "numeric(20,0)", nullable: false),
                    DiscordRoleId = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildClan", x => x.Id);
                    table.UniqueConstraint("AK_GuildClan_ClanId", x => x.ClanId);
                    table.ForeignKey(
                        name: "FK_GuildClan_RegisteredDiscordGuild_GuildId",
                        column: x => x.GuildId,
                        principalSchema: "discord",
                        principalTable: "RegisteredDiscordGuild",
                        principalColumn: "GuildId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuildClanMemberRole",
                schema: "discord_coc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    GuildId = table.Column<ulong>(type: "numeric(20,0)", nullable: false),
                    DiscordRoleId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "GuildMessageCommands",
                schema: "discord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    GuildId = table.Column<ulong>(type: "numeric(20,0)", nullable: false),
                    MessageCommandPrefix = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildMessageCommands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuildMessageCommands_RegisteredDiscordGuild_GuildId",
                        column: x => x.GuildId,
                        principalSchema: "discord",
                        principalTable: "RegisteredDiscordGuild",
                        principalColumn: "GuildId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuildClanMember",
                schema: "discord_coc",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<ulong>(type: "numeric(20,0)", nullable: false),
                    ClanRole = table.Column<int>(type: "integer", nullable: false),
                    ClanId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildClanMember", x => new { x.MemberId, x.UserId });
                    table.ForeignKey(
                        name: "FK_GuildClanMember_DiscordUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "discord",
                        principalTable: "DiscordUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuildClanMember_GuildClan_ClanId",
                        column: x => x.ClanId,
                        principalSchema: "discord_coc",
                        principalTable: "GuildClan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClashOfClansAccount_UserId",
                schema: "discord_coc",
                table: "ClashOfClansAccount",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildClan_GuildId",
                schema: "discord_coc",
                table: "GuildClan",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildClanMember_ClanId",
                schema: "discord_coc",
                table: "GuildClanMember",
                column: "ClanId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildClanMember_UserId",
                schema: "discord_coc",
                table: "GuildClanMember",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildClanMemberRole_GuildId_InGameRole",
                schema: "discord_coc",
                table: "GuildClanMemberRole",
                columns: new[] { "GuildId", "InGameRole" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GuildMessageCommands_GuildId",
                schema: "discord",
                table: "GuildMessageCommands",
                column: "GuildId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClashOfClansAccount",
                schema: "discord_coc");

            migrationBuilder.DropTable(
                name: "GuildClanMember",
                schema: "discord_coc");

            migrationBuilder.DropTable(
                name: "GuildClanMemberRole",
                schema: "discord_coc");

            migrationBuilder.DropTable(
                name: "GuildMessageCommands",
                schema: "discord");

            migrationBuilder.DropTable(
                name: "DiscordUser",
                schema: "discord");

            migrationBuilder.DropTable(
                name: "GuildClan",
                schema: "discord_coc");

            migrationBuilder.DropTable(
                name: "RegisteredDiscordGuild",
                schema: "discord");

            migrationBuilder.DropSequence(
                name: "coc_account_seq",
                schema: "discord_coc");

            migrationBuilder.DropSequence(
                name: "discord_user_seq",
                schema: "discord");

            migrationBuilder.DropSequence(
                name: "guild_clan_member_role_seq",
                schema: "discord_coc");

            migrationBuilder.DropSequence(
                name: "guild_clan_seq",
                schema: "discord_coc");

            migrationBuilder.DropSequence(
                name: "guild_message_commands_seq",
                schema: "discord");

            migrationBuilder.DropSequence(
                name: "registered_discord_guild_seq",
                schema: "discord");
        }
    }
}
