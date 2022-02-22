namespace ClanCommander.ApplicationCore.Entities.Discord.GuildClashOfClans.Clans;

internal class GuildClanMember : Entity
{
    public ClashOfClansPlayerId MemberId { get; private set; }

    public DiscordUserId UserId { get; private set; }

    public ClashOfClansClanRole ClanRole { get; private set; }

    public GuildClanMember(ClashOfClansPlayerId memberId, DiscordUserId userId, ClashOfClansClanRole clanRole)
    {
        MemberId = memberId;
        UserId = userId;
        ClanRole = clanRole;
    }

    public void Promote()
    {
        var newRole = ClanRole.Promote();

        if (newRole is null)
        {
            throw new InvalidOperationException();
        }
        else
        {
            ClanRole = newRole;
        }
    }

    public void Demote()
    {
        var newRole = ClanRole.Demote();

        if (newRole is null)
        {
            throw new InvalidOperationException();
        }
        else
        {
            ClanRole = newRole;
        }
    }
}
