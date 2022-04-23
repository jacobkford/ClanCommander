namespace ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Clans.Events;

internal class DiscordGuildClanAddedEvent : DomainEvent
{
    public ClanId ClanId { get; private set; }

    public string ClanName { get; private set; }

    public DiscordGuildId GuildId { get; private set; }

    public DiscordGuildClanAddedEvent(ClanId clanId, string clanName, DiscordGuildId guildId)
    {
        ClanId = clanId;
        ClanName = clanName;
        GuildId = guildId;
    }
}
