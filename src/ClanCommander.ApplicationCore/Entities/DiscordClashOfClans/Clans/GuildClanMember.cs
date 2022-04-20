namespace ClanCommander.ApplicationCore.Entities.DiscordClashOfClans.Clans;

internal class GuildClanMember : Entity
{
    public PlayerId MemberId { get; private set; }

    public DiscordUserId UserId { get; private set; }

#pragma warning disable CS8618
    // For EF Core
    private GuildClanMember() { }
#pragma warning restore CS8618

    public GuildClanMember(PlayerId memberId, DiscordUserId userId)
    {
        MemberId = memberId;
        UserId = userId;
    }
}
