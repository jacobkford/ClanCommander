namespace ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Clans.Events;

internal class DiscordGuildClanMemberRemovedEvent : DomainEvent
{
    public ClanId ClanId { get; private set; }

    public string ClanName { get; private set; }

    public DiscordGuildId GuildId { get; private set; }

    public PlayerId MemberId { get; private set; }

    public DiscordUserId UserId { get; private set; }

    public DiscordGuildClanMemberRemovedEvent(ClanId clanId, string clanName, DiscordGuildId guildId, PlayerId memberId, DiscordUserId userId)
    {
        ClanId = clanId;
        ClanName = clanName;
        GuildId = guildId;
        MemberId = memberId;
        UserId = userId;
    }
}
