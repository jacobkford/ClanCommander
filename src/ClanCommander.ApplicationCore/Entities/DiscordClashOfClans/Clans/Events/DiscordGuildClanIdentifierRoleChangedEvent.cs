namespace ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Clans.Events;

internal class DiscordGuildClanIdentifierRoleChangedEvent : DomainEvent
{
    public ClanId ClanId { get; private set; }

    public string ClanName { get; private set; }

    public DiscordGuildId GuildId { get; private set; }

    public ulong BeforeRoleId { get; private set; }

    public ulong AfterRoleId { get; private set; }

    public DiscordGuildClanIdentifierRoleChangedEvent(ClanId clanId, string clanName, DiscordGuildId guildId, ulong beforeRoleId, ulong afterRoleId)
    {
        ClanId = clanId;
        ClanName = clanName;
        GuildId = guildId;
        BeforeRoleId = beforeRoleId;
        AfterRoleId = afterRoleId;
    }
}
