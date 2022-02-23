namespace ClanCommander.ApplicationCore.Entities.Discord.GuildClashOfClans.Roles;

internal class GuildClashOfClansRankRole : Entity, IAggregateRoot
{
    public DiscordGuildId GuildId { get; private set; }

    public ulong DiscordRoleId { get; private set; }

    public ClashOfClansClanRole InGameRole { get; private set; }

    public GuildClashOfClansRankRole(
        DiscordGuildId guildId,
        ulong memberDiscordRoleId,
        ClashOfClansClanRole inGameRole)
    {
        Guard.Against.InvalidDiscordSnowflakeId(guildId.Value, nameof(guildId));
        Guard.Against.InvalidDiscordSnowflakeId(memberDiscordRoleId, nameof(memberDiscordRoleId));
        Guard.Against.Null(inGameRole, nameof(inGameRole));

        GuildId = guildId;
        DiscordRoleId = memberDiscordRoleId;
        InGameRole = inGameRole;
    }
    
    public void ChangeDiscordRole(ulong discordRoleId)
    {
        Guard.Against.InvalidDiscordSnowflakeId(discordRoleId, nameof(discordRoleId));

        DiscordRoleId = discordRoleId;
    }
}
