namespace ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Clans;

/// <summary>
/// Discord Guild Role that represents a Clash Of Clans Role
/// </summary>
internal class GuildClanMemberRole : Entity, IAggregateRoot
{
    public DiscordGuildId GuildId { get; private set; }

    public ulong DiscordRoleId { get; private set; }

    public ClanMemberRole InGameRole { get; private set; }

    public GuildClanMemberRole(
        DiscordGuildId guildId,
        ulong memberDiscordRoleId,
        ClanMemberRole inGameRole)
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
